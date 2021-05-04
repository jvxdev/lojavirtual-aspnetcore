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
    [ClientAuthorization]
    [PaymentCookie]
    public class PaymentController : BaseController
    {
        private Cookie _cookie;
        private ManagePagarMe _managePagarMe;
        private IOrderRepository _orderRepository;
        private IOrderSituationRepository _orderSituationRepository;


        public PaymentController
            (
            IOrderRepository orderRepository, 
            IOrderSituationRepository orderSituationRepository, 
            ManagePagarMe managePagarMe, 
            ClientLogin clientLogin, 
            IDeliveryAddressRepository deliveryAddressRepository, 
            IProductRepository productRepository, 
            CookieShoppingKart cookieShoppingKart, 
            CookieFrete cookieValorPrazoFrete, 
            IMapper mapper, WSCorreiosCalcularFrete wsCorreios, 
            CalculatePackage calculatePackage, 
            Cookie cookie
            )
            : 
            base
            (
                clientLogin, 
                deliveryAddressRepository, 
                productRepository, 
                cookieShoppingKart, 
                cookieValorPrazoFrete, 
                mapper, 
                wsCorreios, 
                calculatePackage
            )
        {
            _cookie = cookie;
            _managePagarMe = managePagarMe;
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<ProductItem> productKartItemFull = ReadProductDB();

            ValorPrazoFrete frete = GetFrete();

            ViewBag.Frete = frete;
            ViewBag.Products = productKartItemFull;
            ViewBag.Installments = CalculateInstallment(productKartItemFull);

            return View("Index");
        }


        [HttpPost]
        public IActionResult Index([FromForm] IndexViewModel indexViewModel)
        {
            if (ModelState.IsValid)
            {
                DeliveryAddress deliveryAddress = GetAddress();
                ValorPrazoFrete frete = GetFrete();
                List<ProductItem> products = ReadProductDB();

                Installment installment = LocateInstallment(products, indexViewModel.Installment.Number);

                try
                {
                    Transaction transaction = _managePagarMe.GerarPagCartaoCredito(indexViewModel.CreditCard, installment, deliveryAddress, frete, products);
                    SaveOrder(products, transaction);

                    return new ContentResult() { Content = "Tudo certo! Código da compra com cartão: " + transaction.Id };
                }
                catch (PagarMeException e)
                {
                    TempData["MSG_E"] = CreateErrorMessage(e);

                    return Index();
                }
            }
            else
            {
                return Index();
            }
        }


        private void SaveOrder(List<ProductItem> products, Transaction transaction)
        {
            Order order = new Order();
            order.ClientId = int.Parse(transaction.Customer.Id);
            order.TransactionId = transaction.Id;
            order.FreteCompany = "ECT - Correios";
            order.PaymentForm = (transaction.PaymentMethod == 0) ? "Cartão de crédito" : "Boleto bancário";
            order.TotalValue = GetTotalPurchaseValue(products);
            order.TransactionData = transaction;
            order.ProductsData = products;
            order.RegistryDate = DateTime.Now;
            order.Situation = "";

            _orderRepository.Create(order);

            OrderSituation orderSituation = new OrderSituation();

            orderSituation.OrderId = order.Id;
            orderSituation.Date = DateTime.Now;
            orderSituation.Data = new { Transaction = transaction, Products = products };
            orderSituation.Situation = ;

            _orderSituationRepository.Create(orderSituation);
        }


        public IActionResult BoletoBancario()
        {
            DeliveryAddress deliveryAddress = GetAddress();
            ValorPrazoFrete frete = GetFrete();
            List<ProductItem> products = ReadProductDB();

            var totalPurchaseValue = GetTotalPurchaseValue(products);
            try
            {
                Transaction transaction = _managePagarMe.GerarBoleto(totalPurchaseValue);
                return new ContentResult() { Content = "Tudo certo! Código do boleto: " + transaction.Id };
            }
            catch (PagarMeException e)
            {
                TempData["MSG_E"] = CreateErrorMessage(e);
                return RedirectToAction(nameof(Index));
            }
        }


        private DeliveryAddress GetAddress()
        {
            DeliveryAddress deliveryAddress = null;

            var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));


            if (deliveryAddressId == 0)
            {
                Client client = _clientLogin.getClient();
                deliveryAddress = _mapper.Map<DeliveryAddress>(client);
            }
            else
            {
                var Address = _deliveryAddressRepository.Read(deliveryAddressId);
            }

            return deliveryAddress;
        }


        private ValorPrazoFrete GetFrete()
        {
            var deliveryAddress = GetAddress();
            int cep = int.Parse(Mask.Delete(deliveryAddress.CEP));

            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

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


        private decimal GetTotalPurchaseValue(List<ProductItem> products)
        {
            ValorPrazoFrete frete = GetFrete();

            decimal total = Convert.ToDecimal(frete.Valor);

            foreach (var product in products)
            {
                total += product.Price * product.ItensKartAmount;
            }

            return total;
        }


        private List<SelectListItem> CalculateInstallment(List<ProductItem> products)
        {
            var total = GetTotalPurchaseValue(products);
            var installment = _managePagarMe.CalcularPagamentoParcelado(total);

            return installment.Select(a => new SelectListItem(
                String.Format(
                    "{0}x {1} {2} - Valor total: {3}",
                    a.Number,
                    a.ValuePerInstallment.ToString("C"),
                    (a.Fees) ? "c/ juros" : "s/ juros",
                    a.Value.ToString("C")),
                    a.Number.ToString())).ToList();
        }


        private Installment LocateInstallment(List<ProductItem> products, int number)
        {
            return _managePagarMe.CalcularPagamentoParcelado(GetTotalPurchaseValue(products)).Where(a => a.Number == number).First();
        }


        private string CreateErrorMessage(PagarMeException e)
        {
            StringBuilder sb = new StringBuilder();

            if (e.Error.Errors.Count() > 0)
            {
                sb.Append("Erro no pagamento: ");

                foreach (var erro in e.Error.Errors)
                {
                    sb.Append(erro.Message + "<br />");
                }
            }
            return sb.ToString();
        }
    }
}