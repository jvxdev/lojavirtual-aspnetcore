using AutoMapper;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.AutoMapper;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Models.ViewModel.Payment;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagarMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        private EmailManage _emailManage;


        public PaymentController
            (
            EmailManage emailManage,
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
            _emailManage = emailManage;
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
                    Order order = OrderProcess(products, transaction);

                    return new RedirectToActionResult("Index", "Order", new { id = order.Id });
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


        public IActionResult BoletoBancario()
        {
            DeliveryAddress deliveryAddress = GetAddress();
            ValorPrazoFrete frete = GetFrete();
            List<ProductItem> products = ReadProductDB();

            var totalPurchaseValue = GetTotalPurchaseValue(products);

            try
            {
                Transaction transaction = _managePagarMe.GerarBoleto(totalPurchaseValue, products, deliveryAddress, frete);

                Order order = OrderProcess(products, transaction);

                return new RedirectToActionResult("Index", "Order", new { id = order.Id });
            }
            catch (PagarMeException e)
            {
                TempData["MSG_E"] = CreateErrorMessage(e);

                return RedirectToAction(nameof(Index));
            }
        }


        private Order OrderProcess(List<ProductItem> products, Transaction transaction)
        {
            TransactionPagarMe transactionPagarMe;
            Order order;

            SaveOrder(products, transaction, out transactionPagarMe, out order);
            SaveOrderSituation(products, transactionPagarMe, order);
            StockDischarged(products);

            _cookieShoppingKart.DeleteAll();

            _emailManage.NewOrderEmail(_clientLogin.GetClient(), order);

            return order;
        }


        private void StockDischarged(List<ProductItem> products)
        {
            foreach (var product in products)
            {
                Product productDB = _productRepository.Read(product.Id);
                productDB.Amount -= product.ItensKartAmount;

                _productRepository.Update(productDB);
            }
        }


        private void SaveOrderSituation(List<ProductItem> products, TransactionPagarMe transactionPagarMe, Order order)
        {
            ProductTransaction pt = new ProductTransaction { TransactionPagarMe = transactionPagarMe, Products = products };

            OrderSituation orderSituation = _mapper.Map<Order, OrderSituation>(order);

            orderSituation = _mapper.Map<ProductTransaction, OrderSituation>(pt, orderSituation);

            orderSituation.Situation = OrderSituationConst.PEDIDO_REALIZADO;

            _orderSituationRepository.Create(orderSituation);
        }


        private void SaveOrder(List<ProductItem> products, Transaction transaction, out TransactionPagarMe transactionPagarMe, out Order order)
        {
            transactionPagarMe = _mapper.Map<TransactionPagarMe>(transaction);
            order = _mapper.Map<TransactionPagarMe, Order>(transactionPagarMe);
            order = _mapper.Map<List<ProductItem>, Order>(products, order);

            order.Situation = OrderSituationConst.PEDIDO_REALIZADO;

            _orderRepository.Create(order);
        }


        private DeliveryAddress GetAddress()
        {
            DeliveryAddress deliveryAddress = null;

            var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));

            if (deliveryAddressId == 0)
            {
                Client client = _clientLogin.GetClient();
                deliveryAddress = _mapper.Map<DeliveryAddress>(client);
            }
            else
            {
                deliveryAddress = _deliveryAddressRepository.Read(deliveryAddressId);
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

            return null;
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