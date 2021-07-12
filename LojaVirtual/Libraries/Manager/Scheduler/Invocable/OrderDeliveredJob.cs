using Coravel.Invocable;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Manager.Scheduler.Invocable
{
    public class OrderDeliveredJob : IInvocable
    {
        private IOrderRepository _orderRepository;
        private IOrderSituationRepository _orderSituationRepository;
        private ILogger<OrderPaymentSituationJob> _ilogger;


        public OrderDeliveredJob(IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository, ILogger<OrderPaymentSituationJob> ilogger)
        {
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
            _ilogger = ilogger;
        }


        public Task Invoke()
        {
            var orders = _orderRepository.GetAllOrdersBySituation(OrderSituationConst.EM_TRANSPORTE);
            
            foreach(var order in orders)
            {
                _ilogger.LogInformation("> OrderDeliveredJob: Iniciando <");

                var result = new Correios.NET.Services().GetPackageTracking(order.FreteTrackingCod);

                if (result.IsDelivered)
                {
                    OrderSituation orderSituation = new OrderSituation();

                    orderSituation.OrderId = order.Id;
                    orderSituation.Situation = OrderSituationConst.ENTREGUE;
                    orderSituation.Date = result.DeliveryDate.Value;
                    orderSituation.Data = JsonConvert.SerializeObject(result);

                    _orderSituationRepository.Create(orderSituation);

                    order.Situation = OrderSituationConst.ENTREGUE;

                    _orderRepository.Update(order);
                }
            }

            _ilogger.LogInformation("> OrderDeliveredJob: Finalizado <");

            return Task.CompletedTask;
        }
    }
}
