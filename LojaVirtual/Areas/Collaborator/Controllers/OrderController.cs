using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Models.ViewModel.Order;
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


        public IActionResult Index(int? page, string codOrder, string cpf)
        {
            var orders = _orderRepository.ReadAllOrders(page, codOrder, cpf);

            return View(orders);
        }


        public IActionResult Show(int Id)
        {
            Order order = _orderRepository.Read(Id);

            var viewModel = new ShowViewModel() { Order = order };

            return View(viewModel);
        }


        public IActionResult NFE([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("Refund");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                string Url = viewModel.NFE.NFE_Url;

                Order order = _orderRepository.Read(id);

                order.NFE = Url;
                order.Situation = OrderSituationConst.NF_EMITIDA;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = Url;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.NF_EMITIDA;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_NFE = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult TrackingCod([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("Refund");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                string trackingCod = viewModel.TrackingCod.Code;

                Order order = _orderRepository.Read(id);

                order.FreteTrackingCod = trackingCod;
                order.Situation = OrderSituationConst.EM_TRANSPORTE;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = trackingCod;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.EM_TRANSPORTE;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                return RedirectToAction(nameof(Show), new { Id = id });

            }

            ViewBag.MODAL_TRACKING_COD = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderCreditCard([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("Refund");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                viewModel.CreditCard.PaymentForm = PaymentMethodConst.CreditCard;

                Order order = _orderRepository.Read(id);

                _managePagarMe.EstornoCreditCard(order.TransactionId);

                order.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.CreditCard);
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                ProductsRefundStock(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_CREDIT_CARD = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderBoletoBancario([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("Refund");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                viewModel.BoletoBancario.PaymentForm = PaymentMethodConst.Boleto;

                Order order = _orderRepository.Read(id);

                _managePagarMe.EstornoBoletoBancario(order.TransactionId, viewModel.BoletoBancario);

                order.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.BoletoBancario);
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                ProductsRefundStock(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_BOLETO = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrder([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                Order order = _orderRepository.Read(id);

                order.Situation = OrderSituationConst.DEVOLUCAO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.Refund);
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderRejected([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("Refund");

            if (ModelState.IsValid)
            {
                Order order = _orderRepository.Read(id);

                order.Situation = OrderSituationConst.DEVOLUCAO_REJEITADA;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = viewModel.RefundRejectReason;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_REJEITADA;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND_REJECT = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderApprovedCreditCard(int id)
        {
            Order order = _orderRepository.Read(id);

            if (order.Situation == OrderSituationConst.DEVOLUCAO_ENTREGUE)
            {
                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_APROVADA;

                _orderSituationRepository.Create(orderSituation);

                _managePagarMe.EstornoCreditCard(order.TransactionId);

                orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                ProductsRefundStock(order);

                order.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderRepository.Update(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ShowViewModel viewModel = new ShowViewModel();

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderApprovedBoletoBancario([FromForm] ShowViewModel viewModel, int id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("Refund");
            ModelState.Remove("RefundRejectReason");
            ModelState.Remove("BoletoBancario.CancelReason");

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_APROVADA;

                _orderSituationRepository.Create(orderSituation);

                viewModel.BoletoBancario.PaymentForm = PaymentMethodConst.Boleto;

                _managePagarMe.EstornoBoletoBancario(order.TransactionId, viewModel.BoletoBancario);

                orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.BoletoBancario);
                orderSituation.OrderId = id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                order.Situation = OrderSituationConst.DEVOLUCAO_ESTORNO;

                _orderRepository.Update(order);

                ProductsRefundStock(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND_BOLETO = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        private void ProductsRefundStock(Order order)
        {
            List<ProductItem> products = JsonConvert.DeserializeObject<List<ProductItem>>(order.ProductsData, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>() });

            foreach (var product in products)
            {
                Product productDB = _productRepository.Read(product.Id);

                productDB.Stock += product.ChosenUnits;

                _productRepository.Update(productDB);
            }
        }
    }
}
