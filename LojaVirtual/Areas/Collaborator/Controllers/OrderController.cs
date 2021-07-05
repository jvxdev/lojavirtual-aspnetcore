﻿using LojaVirtual.Libraries.Filters;
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


        public IActionResult Index(int? Page, string codOrder, string Cpf)
        {
            var orders = _orderRepository.ReadAllOrders(Page, codOrder, Cpf);

            return View(orders);
        }


        public IActionResult Show(int Id)
        {
            Order order = _orderRepository.Read(Id);

            var viewModel = new ShowViewModel() { Order = order };

            return View(viewModel);
        }


        public IActionResult NFE([FromForm] ShowViewModel viewModel, int Id)
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
            }
            else
            {
                ViewBag.MODAL_NFE = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult TrackingCod([FromForm] ShowViewModel viewModel, int Id)
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

            }
            else
            {
                ViewBag.MODAL_TRACKING_COD = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderCreditCard([FromForm] ShowViewModel viewModel, int Id)
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

                Order order = _orderRepository.Read(Id);

                _managePagarMe.EstornoCreditCard(order.TransactionId);

                order.Situation = OrderSituationConst.ESTORNO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.CreditCard);
                orderSituation.OrderId = Id;
                orderSituation.Situation = OrderSituationConst.ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                ProductsRefundStock(order);
            }
            else
            {
                ViewBag.MODAL_CREDIT_CARD = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderBoletoBancario([FromForm] ShowViewModel viewModel, int Id)
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

                Order order = _orderRepository.Read(Id);

                _managePagarMe.EstornoBoletoBancario(order.TransactionId, viewModel.BoletoBancario);

                order.Situation = OrderSituationConst.ESTORNO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.BoletoBancario);
                orderSituation.OrderId = Id;
                orderSituation.Situation = OrderSituationConst.ESTORNO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);

                ProductsRefundStock(order);
            }
            else
            {
                ViewBag.MODAL_BOLETO = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrder([FromForm] ShowViewModel viewModel, int Id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");
            ModelState.Remove("RefundRejectReason");

            if (ModelState.IsValid)
            {
                Order order = _orderRepository.Read(Id);

                order.Situation = OrderSituationConst.DEVOLUCAO;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = JsonConvert.SerializeObject(viewModel.Refund);
                orderSituation.OrderId = Id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);
            }
            else
            {
                ViewBag.MODAL_REFUND = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderRejected([FromForm] ShowViewModel viewModel, int Id)
        {
            ModelState.Remove("Order");
            ModelState.Remove("NFE");
            ModelState.Remove("TrackingCod");
            ModelState.Remove("CreditCard");
            ModelState.Remove("BoletoBancario");

            if (ModelState.IsValid)
            {
                Order order = _orderRepository.Read(Id);

                order.Situation = OrderSituationConst.DEVOLUCAO_REJEITADA;

                var orderSituation = new OrderSituation();
                orderSituation.Date = DateTime.Now;
                orderSituation.Data = viewModel.RefundRejectReason;
                orderSituation.OrderId = Id;
                orderSituation.Situation = OrderSituationConst.DEVOLUCAO_REJEITADA;

                _orderSituationRepository.Create(orderSituation);

                _orderRepository.Update(order);
            }
            else
            {
                ViewBag.MODAL_REFUND_REJECT = true;
            }

            viewModel.Order = _orderRepository.Read(Id);

            return View(nameof(Show), viewModel);
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
