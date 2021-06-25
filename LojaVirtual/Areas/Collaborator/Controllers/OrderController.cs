using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private IOrderSituationRepository _orderSituationRepository;
        private IProductRepository _productRepository;
        private ManagePagarMe _managePagarMe;


        public OrderController(IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository, IProductRepository productRepository, ManagePagarMe managePagarMe)
        {
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
            _managePagarMe = managePagarMe;
            _productRepository = productRepository;
        }


        public IActionResult Index(int? Page, string codOrder, string Cpf)
        {
            var orders = _orderRepository.ReadAllOrders(Page, codOrder, Cpf);

            return View(orders);
        }


        public IActionResult Show(int Id)
        {
            Order order = _orderRepository.Read(Id);

            return View(order);
        }


        public IActionResult NFE(int Id)
        {
            string Url = HttpContext.Request.Form["nfe_url"];

            Order order = _orderRepository.Read(Id);

            order.NFE = Url;
            order.Situation = OrderSituationConst.NF_EMITIDA;

            var orderSituation = new OrderSituation();
            orderSituation.Date = DateTime.Now;
            orderSituation.Data = Url;
            orderSituation.OrderId = Id;
            orderSituation.Situation = OrderSituationConst.NF_EMITIDA;

            _orderSituationRepository.Create(orderSituation);

            _orderRepository.Update(order);

            return RedirectToAction(nameof(Show), new { Id = Id });
        }


        public IActionResult TrackingCod(int Id)
        {
            string trackingCod = HttpContext.Request.Form["tracking_cod"];

            Order order = _orderRepository.Read(Id);

            order.FreteTrackingCod = trackingCod;
            order.Situation = OrderSituationConst.EM_TRANSPORTE;

            var orderSituation = new OrderSituation();
            orderSituation.Date = DateTime.Now;
            orderSituation.Data = trackingCod;
            orderSituation.OrderId = Id;
            orderSituation.Situation = OrderSituationConst.EM_TRANSPORTE;

            _orderSituationRepository.Create(orderSituation);

            _orderRepository.Update(order);

            return RedirectToAction(nameof(Show), new { Id = Id });
        }


        public IActionResult CancelOrderCreditCard(int Id)
        {
            string reason = HttpContext.Request.Form["cancel_reason"];

            Order order = _orderRepository.Read(Id);

            _managePagarMe.EstornoCreditCard(order.TransactionId);

            order.Situation = OrderSituationConst.ESTORNO;

            var orderSituation = new OrderSituation();
            orderSituation.Date = DateTime.Now;
            orderSituation.Data = JsonConvert.SerializeObject(new CancelData() { CancelReason = reason });
            orderSituation.OrderId = Id;
            orderSituation.Situation = OrderSituationConst.ESTORNO;

            _orderSituationRepository.Create(orderSituation);

            _orderRepository.Update(order);

            ProductsRefundStock(order);

            return RedirectToAction(nameof(Show), new { Id = Id });
        }


        private void ProductsRefundStock(Order order)
        {
            List<ProductItem> products = JsonConvert.DeserializeObject<List<ProductItem>>(order.ProductsData, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>() });

            foreach (var product in products)
            {
                Product productDB = _productRepository.Read(product.Id);

                productDB.Amount += product.ItensKartAmount;

                _productRepository.Update(productDB);
            }
        }
    }
}
