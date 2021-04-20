using AutoMapper;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Manager.Payment;
using PagarMe;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojaVirtual.Models.ViewModel.Payment;

namespace LojaVirtual.Controllers
{
    public class PaymentController : BaseController
    {
        private Cookie _cookie;
        private ManagePagarMe _managePagarMe;


        public PaymentController(ManagePagarMe managePagarMe, ClientLogin clientLogin, IDeliveryAddressRepository deliveryAddressRepository, IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage, Cookie cookie) : base(clientLogin, deliveryAddressRepository, productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
            _cookie = cookie;
            _managePagarMe = managePagarMe;
        }


        [HttpGet]
        [ClientAuthorization]
        public IActionResult Index()
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            if (tipoFreteSelected != null)
            {
                var deliveryAddress = GetAddress();
                var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());
                int cep = int.Parse(Mask.Delete(deliveryAddress.CEP));
                List<ProductItem> productKartItemFull = ReadProductDB();

                var frete = GetFrete(cep.ToString());
                var total = GetTotalPurchaseValue(productKartItemFull, frete);
                var installment = _managePagarMe.CalcularPagamentoParcelado(total);

                ViewBag.Frete = frete;
                ViewBag.Products = productKartItemFull;
                ViewBag.Installments = installment.Select(a => new SelectListItem(
                    String.Format(
                        "{0}x {1} {2} - Valor total: {3}",
                        a.Number, 
                        a.ValuePerInstallment.ToString("C"),
                        (a.Fees) ? "c/ juros" : "s/ juros",
                        a.Value.ToString("C")),
                        a.Number.ToString())).ToList();

                return View("Index");
            }

            TempData["MSG_E"] = Message.MSG_E010;
            return RedirectToAction("DeliveryAddress", "ShoppingKart");
        }


        [HttpPost]
        [ClientAuthorization]
        public IActionResult Index([FromForm] IndexViewModel indexViewModel)
        {
            if (ModelState.IsValid)
            {
                DeliveryAddress deliveryAddress = GetAddress();
                ValorPrazoFrete frete = GetFrete(deliveryAddress.CEP.ToString());
                List<ProductItem> products = ReadProductDB();

                var installment = _managePagarMe.CalcularPagamentoParcelado(GetTotalPurchaseValue(products, frete)).Where(a => a.Number == indexViewModel.Installment.Number).First();

                try
                {
                    dynamic pagarMeReturn = _managePagarMe.GerarPagCartaoCredito(indexViewModel.CreditCard, installment, deliveryAddress, frete, products);
                    
                    return new ContentResult() { Content = "Tudo certo! " + pagarMeReturn.TransactionId };
                }
                catch (PagarMeException e)
                {
                    StringBuilder sb = new StringBuilder();

                    if (e.Error.Errors.Count() > 0)
                    {
                        sb.Append("Erro no pagamento: ");

                        foreach(var erro in e.Error.Errors)
                        {
                            sb.Append(erro.Message + "<br />");
                        }
                    }
                    TempData["MSG_E"] = sb.ToString();

                    return Index();
                }
            }
            else
            {
                return Index();
            }
        }


        private DeliveryAddress GetAddress()
        {
            DeliveryAddress deliveryAddress = null;

            var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));


            if (deliveryAddressId == 0)
            {
                Client client = _clientLogin.getClient();

                deliveryAddress = new DeliveryAddress();
                deliveryAddress.AddressName = "Endereço do cliente";
                deliveryAddress.Id = 0;
                deliveryAddress.CEP = client.CEP;
                deliveryAddress.State = client.State;
                deliveryAddress.City = client.City;
                deliveryAddress.Neighborhood = client.Neighborhood;
                deliveryAddress.Street = client.Street;
                deliveryAddress.HouseNumber = client.HouseNumber;
            }
            else
            {
                var Address = _deliveryAddressRepository.Read(deliveryAddressId);
            }

            return deliveryAddress;
        }


        private ValorPrazoFrete GetFrete(string cepDestino)
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

            int cep = int.Parse(Mask.Delete(cepDestino));

            Frete frete = _cookieFrete.Read().Where(a => a.CEP == cep && a.codShoppingKart == shoppingKartHash).FirstOrDefault();

            if (frete != null)
            {
                return frete.valuesList.Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }


        private decimal GetTotalPurchaseValue(List<ProductItem> products, ValorPrazoFrete frete)
        {
            decimal total = Convert.ToDecimal(frete.Valor);

            foreach(var product in products)
            {
                total += product.Price;
            }

            return total;
        }
    }
}