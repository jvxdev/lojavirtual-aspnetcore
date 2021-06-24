using Coravel.Invocable;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
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


        public OrderDeliveredJob(IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository)
        {
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
        }


        public Task Invoke()
        {
            var orders = _orderRepository.GetAllOrdersBySituation(OrderSituationConst.EM_TRANSPORTE);
            
            foreach(var order in orders)
            {
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

            return Task.CompletedTask;
        }
    }
}
