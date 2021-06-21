using AutoMapper;
using Coravel.Invocable;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PagarMe;
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
        private IOrderSituationRepository _orderSituationRepository;
        private IMapper _mapper;
        private IConfiguration _conf;


        public OrderPaymentSituationJob(ManagePagarMe managePagarMe, IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository, IMapper mapper, IConfiguration conf)
        {
            _managePagarMe = managePagarMe;
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
            _mapper = mapper;
            _conf = conf;
        }


        public Task Invoke()
        {
            var ordersPlaced = _orderRepository.GetAllOrdersPlaced();

            foreach (var order in ordersPlaced)
            {
                string situation = null;

                var transaction = _managePagarMe.GetTransaction(order.TransactionId);

                int ToleranceDays =_conf.GetValue<int>("Payment:PagarMe:BoletoExpirationDays") + _conf.GetValue<int>("Payment:PagarMe:BoletoToleranceDays");

                if (transaction.Status == TransactionStatus.WaitingPayment && transaction.PaymentMethod == PaymentMethod.Boleto && DateTime.Now > order.RegistryDate.AddDays(ToleranceDays))
                {
                    situation = OrderSituationConst.PAGAMENTO_NAO_REALIZADO;
                }

                if (transaction.Status == TransactionStatus.Refused)
                {
                    situation = OrderSituationConst.PAGAMENTO_REJEITADO;
                }

                if (transaction.Status == TransactionStatus.Authorized || transaction.Status == TransactionStatus.Paid)
                {
                    situation = OrderSituationConst.PAGAMENTO_APROVADO;
                }
                    
                if (situation != null)
                {
                    TransactionPagarMe transactionPagarMe = _mapper.Map<Transaction, TransactionPagarMe>(transaction);

                    transactionPagarMe.Customer.Gender = (order.Client.Sex == "M") ? Gender.Male : Gender.Female;

                    OrderSituation orderSituation = new OrderSituation();

                    orderSituation.OrderId = order.Id;
                    orderSituation.Situation = situation;
                    orderSituation.Date = transaction.DateUpdated.Value;
                    orderSituation.Data = JsonConvert.SerializeObject(transactionPagarMe);

                    _orderSituationRepository.Create(orderSituation);

                    order.Situation = OrderSituationConst.PAGAMENTO_APROVADO;

                    _orderRepository.Update(order);
                }
            }

            Debug.WriteLine("----- OrderPaymentSituationJob - Executado -----");

            return Task.CompletedTask;
        }
    }
}
