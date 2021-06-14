using Coravel.Invocable;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Manager.Scheduler.Invocable
{
    public class OrderPaymentSituationJob : IInvocable
    {
        private ManagePagarMe _managePagarMe;
        private IOrderRepository _orderRepository;


        public OrderPaymentSituationJob(ManagePagarMe managePagarMe, IOrderRepository orderRepository)
        {
            _managePagarMe = managePagarMe;
            _orderRepository = orderRepository;
        }


        public Task Invoke()
        {
            var ordersPlaced = _orderRepository.GetAllOrdersPlaced();

            foreach (var orders in ordersPlaced)
            {
                var transaction = _managePagarMe.GetTransaction(orders.TransactionId);
            }

            /*
             if (transaction.Status == TransactionStatus.Authorized || transaction.Status == TransactionStatus.Paid)
            {
                transaction.DateUpdated;
            }
             */

            Debug.WriteLine("----- OrderPaymentSituationJob - Executado -----");

            return Task.CompletedTask;
        }
    }
}
