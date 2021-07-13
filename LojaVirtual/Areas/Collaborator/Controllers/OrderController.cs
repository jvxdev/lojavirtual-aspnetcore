using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Models.ViewModel.Order;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

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
            FormValidate(nameof(viewModel.NFE));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                string url = viewModel.NFE.NFE_Url;

                SaveOrderSituation(id, url, OrderSituationConst.NF_EMITIDA);

                SaveOrder(order, url, OrderSituationConst.NF_EMITIDA);
                
                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_NFE = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult TrackingCod([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.TrackingCod));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                string trackingCod = viewModel.TrackingCod.Code;

                SaveOrderSituation(id, trackingCod, OrderSituationConst.EM_TRANSPORTE);

                SaveOrder(order, trackingCod, OrderSituationConst.EM_TRANSPORTE);

                return RedirectToAction(nameof(Show), new { Id = id });

            }

            ViewBag.MODAL_TRACKING_COD = true;

            viewModel.Order = _orderRepository.Read(id);

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderCreditCard([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.CreditCard));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                viewModel.CreditCard.PaymentForm = PaymentMethodConst.CreditCard;

                _managePagarMe.EstornoCreditCard(order.TransactionId);

                SaveOrderSituation(id, viewModel.CreditCard, OrderSituationConst.ESTORNO);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO_ESTORNO);

                _productRepository.ProductsRefundStock(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_CREDIT_CARD = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult CancelOrderBoletoBancario([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.BoletoBancario));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                viewModel.BoletoBancario.PaymentForm = PaymentMethodConst.Boleto;

                _managePagarMe.EstornoBoletoBancario(order.TransactionId, viewModel.BoletoBancario);

                SaveOrderSituation(id, viewModel.BoletoBancario, OrderSituationConst.DEVOLUCAO_ESTORNO);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO_ESTORNO);

                _productRepository.ProductsRefundStock(order);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_BOLETO = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrder([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.Refund));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                SaveOrderSituation(id, viewModel.Refund, OrderSituationConst.DEVOLUCAO);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderRejected([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.RefundRejectReason));

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                SaveOrderSituation(id, viewModel.RefundRejectReason, OrderSituationConst.DEVOLUCAO_REJEITADA);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO_REJEITADA);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND_REJECT = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderApprovedCreditCard(int id)
        {
            Order order = _orderRepository.Read(id);

            if (order.Situation == OrderSituationConst.DEVOLUCAO_ENTREGUE)
            {
                _managePagarMe.EstornoCreditCard(order.TransactionId);
                _productRepository.ProductsRefundStock(order);

                SaveOrderSituation(id, "", OrderSituationConst.DEVOLUCAO_APROVADA);

                SaveOrderSituation(id, "", OrderSituationConst.DEVOLUCAO_ESTORNO);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO_ESTORNO);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ShowViewModel viewModel = new ShowViewModel();

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        public IActionResult RefundOrderApprovedBoletoBancario([FromForm] ShowViewModel viewModel, int id)
        {
            FormValidate(nameof(viewModel.BoletoBancario), "BoletoBancario.CancelReason");

            Order order = _orderRepository.Read(id);

            if (ModelState.IsValid)
            {
                viewModel.BoletoBancario.PaymentForm = PaymentMethodConst.Boleto;

                _managePagarMe.EstornoBoletoBancario(order.TransactionId, viewModel.BoletoBancario);
                _productRepository.ProductsRefundStock(order);

                SaveOrderSituation(id, "", OrderSituationConst.DEVOLUCAO_APROVADA);

                SaveOrderSituation(id, viewModel.BoletoBancario, OrderSituationConst.DEVOLUCAO_ESTORNO);

                SaveOrder(order, OrderSituationConst.DEVOLUCAO_ESTORNO);

                return RedirectToAction(nameof(Show), new { Id = id });
            }

            ViewBag.MODAL_REFUND_BOLETO = true;

            viewModel.Order = order;

            return View(nameof(Show), viewModel);
        }


        private void FormValidate(string formValidate, params string[] removeFromForms)
        {
            var properties = new ShowViewModel().GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.Name != formValidate)
                {
                    ModelState.Remove(property.Name);
                }
            }

            foreach (string removeFromForm in removeFromForms)
            {
                ModelState.Remove(removeFromForm);
            }
        }


        private void SaveOrderSituation(int orderId, object data, string situation)
        {
            var orderSituation = new OrderSituation();
            orderSituation.Date = DateTime.Now;
            orderSituation.Data = JsonConvert.SerializeObject(data);
            orderSituation.OrderId = orderId;
            orderSituation.Situation = situation;

            _orderSituationRepository.Create(orderSituation);
        }


        private void SaveOrder(Order order, string situation, string nfe = null, string trackingCod = null)
        {
            order.Situation = situation;

            if (nfe != null)
            {
                order.NFE = nfe;
            }

            if (trackingCod != null)
            {
                order.FreteTrackingCod = trackingCod;
            }

            _orderRepository.Update(order);
        }
    }
}