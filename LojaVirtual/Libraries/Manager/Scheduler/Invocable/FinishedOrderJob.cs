using Coravel.Invocable;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Manager.Scheduler.Invocable
{
    public class FinishedOrderJob : IInvocable
    {
        private IOrderRepository _orderRepository;
        private IOrderSituationRepository _orderSituationRepository;
        private IConfiguration _conf;


        public FinishedOrderJob(IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository, IConfiguration conf)
        {
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
            _conf = conf;
        }


        public Task Invoke()
        {
            var orders = _orderRepository.GetAllOrdersBySituation(OrderSituationConst.ENTREGUE);
        
            foreach(var order in orders)
            {
                var orderSituationDB = order.OrderSituations.FirstOrDefault(a => a.Situation == OrderSituationConst.ENTREGUE);
            
                if (orderSituationDB != null)
                {
                    int tolerance = _conf.GetValue<int>("Finished:Days");

                    if (DateTime.Now >= orderSituationDB.Date.AddDays(tolerance))
                    {
                        OrderSituation orderSituation = new OrderSituation();

                        orderSituation.OrderId = order.Id;
                        orderSituation.Situation = OrderSituationConst.FINALIZADO;
                        orderSituation.Date = DateTime.Now;
                        orderSituation.Data = string.Empty;

                        _orderSituationRepository.Create(orderSituation);

                        order.Situation = OrderSituationConst.FINALIZADO;

                        _orderRepository.Update(order);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
