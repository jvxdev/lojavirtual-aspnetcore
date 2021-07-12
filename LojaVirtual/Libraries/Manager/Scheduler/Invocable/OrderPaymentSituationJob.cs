using AutoMapper;
using Coravel.Invocable;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Libraries.Manager.Payment;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private IConfiguration _conf;
        private ILogger<OrderPaymentSituationJob> _ilogger;


        public OrderPaymentSituationJob(ManagePagarMe managePagarMe, IOrderRepository orderRepository, IOrderSituationRepository orderSituationRepository, IProductRepository productRepository, IMapper mapper, IConfiguration conf, ILogger<OrderPaymentSituationJob> ilogger)
        {
            _managePagarMe = managePagarMe;
            _orderRepository = orderRepository;
            _orderSituationRepository = orderSituationRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _conf = conf;
            _ilogger = ilogger;
        }


        public Task Invoke()
        {
            _ilogger.LogInformation("> OrderPaymentSituationJob: Iniciando <");

            var ordersPlaced = _orderRepository.GetAllOrdersBySituation(OrderSituationConst.PEDIDO_REALIZADO) ;

            foreach (var order in ordersPlaced)
            {
                string situation = null;

                var transaction = _managePagarMe.GetTransaction(order.TransactionId);

                int ToleranceDays =_conf.GetValue<int>("Payment:PagarMe:BoletoExpirationDays") + _conf.GetValue<int>("Payment:PagarMe:BoletoToleranceDays");

                if (transaction.Status == TransactionStatus.WaitingPayment && transaction.PaymentMethod == PaymentMethod.Boleto && DateTime.Now > order.RegistryDate.AddDays(ToleranceDays))
                {
                    situation = OrderSituationConst.PAGAMENTO_NAO_REALIZADO;

                    ProductsRefundStock(order); 
                }

                if (transaction.Status == TransactionStatus.Refused)
                {
                    situation = OrderSituationConst.PAGAMENTO_REJEITADO;

                    ProductsRefundStock(order);
                }

                if (transaction.Status == TransactionStatus.Authorized || transaction.Status == TransactionStatus.Paid)
                {
                    situation = OrderSituationConst.PAGAMENTO_APROVADO;
                }

                if (transaction.Status == TransactionStatus.Refunded || transaction.Status == TransactionStatus.Refunded)
                {
                    situation = OrderSituationConst.ESTORNO;

                    ProductsRefundStock(order);
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

                    order.Situation = situation;

                    _orderRepository.Update(order);
                }
            }

            _ilogger.LogInformation("> OrderPaymentSituationJob: Finalizado <");

            return Task.CompletedTask;
        }


        private void ProductsRefundStock(Order order)
        {
            List<ProductItem> products = JsonConvert.DeserializeObject<List<ProductItem>>(order.ProductsData, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>() });
        
            foreach(var product in products)
            {
                Product productDB = _productRepository.Read(product.Id);

                productDB.Amount += product.ItensKartAmount;

                _productRepository.Update(productDB);
            }
        }
    }
}