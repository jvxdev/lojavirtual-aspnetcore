using Coravel.Invocable;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
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

            }

            return Task.CompletedTask;
        }
    }
}
