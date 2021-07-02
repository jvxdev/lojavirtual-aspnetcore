using AutoMapper;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using Microsoft.Extensions.Configuration;
using PagarMe;
using System;
using System.Collections.Generic;

namespace LojaVirtual.Libraries.Manager.Payment
{
    public class ManagePagarMe
    {
        private IConfiguration _conf;
        private ClientLogin _clientLogin;
        private IMapper _mapper;


        public ManagePagarMe(IConfiguration conf, ClientLogin clientLogin, IMapper mapper)
        {
            _conf = conf;
            _clientLogin = clientLogin;
            _mapper = mapper;
        }


        public Transaction GerarBoleto(decimal value, List<ProductItem> products, DeliveryAddress deliveryAddress, ValorPrazoFrete valorFrete)
        {
            Client client = _clientLogin.GetClient();

            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = _conf.GetValue<String>("Payment:PagarMe:EcryptionKey");
            int ExpirationDays = _conf.GetValue<int>("Payment:PagarMe:BoletoExpirationDays");

            Transaction transaction = new Transaction();

            transaction.Amount = Convert.ToInt32(value);
            transaction.PaymentMethod = PaymentMethod.Boleto;
            transaction.BoletoExpirationDate = DateTime.Now.AddDays(ExpirationDays);

            transaction.Customer = new Customer
            {
                ExternalId = client.Id.ToString(),
                Name = client.Name,
                Type = CustomerType.Individual,
                Country = "br",
                Email = client.Email,
                Documents = new[] {
                    new Document {
                        Type = DocumentType.Cpf,
                        Number = Mask.Delete(client.CPF)
                    }
                },
                PhoneNumbers = new string[]
                {
                    "+55" + Mask.Delete(client.Phone)
                },
                Birthday = client.BirthDate?.ToString("yyyy-MM-dd")
            };

            var Today = DateTime.Now;

            var Fee = Convert.ToDecimal(valorFrete.Valor);

            transaction.Shipping = new PagarMe.Shipping
            {
                Name = deliveryAddress.AddressName,
                Fee = Mask.ConvertValuePagarMe(Fee),
                DeliveryDate = Today.AddDays(_conf.GetValue<int>("diasNaEmpresa")).AddDays(valorFrete.Prazo).ToString("yyyy-MM-dd"),
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = deliveryAddress.State,
                    City = deliveryAddress.City,
                    Neighborhood = deliveryAddress.Neighborhood,
                    Street = deliveryAddress.Street,
                    StreetNumber = deliveryAddress.HouseNumber,
                    Zipcode = Mask.Delete(deliveryAddress.CEP)
                }
            };

            Item[] items = new Item[products.Count];

            for (var i = 0; i < products.Count; i++)
            {
                var item = products[i];
                var itemA = new Item()
                {
                    Id = item.Id.ToString(),
                    Title = item.Name,
                    Quantity = item.ItensKartAmount,
                    Tangible = true,
                    UnitPrice = Mask.ConvertValuePagarMe(item.Price),
                };

                items[i] = itemA;
            }

            transaction.Item = items;

            transaction.Amount = Mask.ConvertValuePagarMe(value);

            transaction.Save();

            transaction.Customer.Gender = (client.Sex == "M") ? Gender.Male : Gender.Female;

            return transaction;
        }


        public Transaction GerarPagCartaoCredito(CreditCard creditCard, Installment installment, DeliveryAddress deliveryAddress, ValorPrazoFrete valorFrete, List<ProductItem> products)
        {
            Client client = _clientLogin.GetClient();

            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = _conf.GetValue<String>("Payment:PagarMe:EcryptionKey");

            Card card = new Card();
            card.Number = Mask.Delete(creditCard.CardNumber);
            card.HolderName = creditCard.CardHolderName;
            card.ExpirationDate = creditCard.CardExpirationDateMM + creditCard.CardExpirationDateYY;
            card.Cvv = creditCard.CardCvv;

            card.Save();

            Transaction transaction = new Transaction();
            transaction.PaymentMethod = PaymentMethod.CreditCard;

            transaction.Card = new Card
            {
                Id = card.Id
            };

            transaction.Customer = new Customer
            {
                ExternalId = client.Id.ToString(),
                Name = client.Name,
                Type = CustomerType.Individual,
                Country = "br",
                Email = client.Email,
                Documents = new[]
              {
                    new Document{
                      Type = DocumentType.Cpf,
                      Number = Mask.Delete(client.CPF)
                    }
                  },
                PhoneNumbers = new string[]
              {
                "+55" + Mask.Delete(client.Phone)
              },
                Birthday = client.BirthDate?.ToString("yyyy-MM-dd")
            };

            transaction.Billing = new Billing
            {
                Name = client.Name,
                Address = new Address()
                {
                    Country = "br",
                    State = client.State,
                    City = client.City,
                    Neighborhood = client.Neighborhood,
                    Street = client.Street,
                    StreetNumber = client.HouseNumber,
                    Zipcode = Mask.Delete(client.CEP)
                }
            };

            var Today = DateTime.Now;

            var Fee = Convert.ToDecimal(valorFrete.Valor);

            transaction.Shipping = new PagarMe.Shipping
            {
                Name = deliveryAddress.AddressName,
                Fee = Mask.ConvertValuePagarMe(Fee),
                DeliveryDate = Today.AddDays(_conf.GetValue<int>("diasNaEmpresa")).AddDays(valorFrete.Prazo).ToString("yyyy-MM-dd"),
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = deliveryAddress.State,
                    City = deliveryAddress.City,
                    Neighborhood = deliveryAddress.Neighborhood,
                    Street = deliveryAddress.Street,
                    StreetNumber = deliveryAddress.HouseNumber,
                    Zipcode = Mask.Delete(deliveryAddress.CEP)
                }
            };

            Item[] items = new Item[products.Count];

            for (var i = 0; i < products.Count; i++)
            {
                var item = products[i];
                var itemA = new Item()
                {
                    Id = item.Id.ToString(),
                    Title = item.Name,
                    Quantity = item.ItensKartAmount,
                    Tangible = true,
                    UnitPrice = Mask.ConvertValuePagarMe(item.Price),
                };

                items[i] = itemA;
            }

            transaction.Item = items;

            transaction.Amount = Mask.ConvertValuePagarMe(installment.Value);

            transaction.Installments = installment.Number;

            transaction.Save();

            transaction.Customer.Gender = (client.Sex == "M") ? Gender.Male : Gender.Female;

            return transaction;
        }


        public List<Installment> CalcularPagamentoParcelado(decimal value)
        {
            List<Installment> list = new List<Installment>();

            int maxInstallments = _conf.GetValue<int>("Payment:PagarMe:MaxInstallments");
            int sellerInstallment = _conf.GetValue<int>("Payment:PagarMe:SellerInstallment");
            decimal fees = _conf.GetValue<decimal>("Payment:PagarMe:Fees");

            for (int i = 1; i <= maxInstallments; i++)
            {
                Installment installment = new Installment();

                installment.Number = i;

                if (i > sellerInstallment)
                {
                    int installmentsFeesAmount = i - sellerInstallment;
                    decimal feesValue = value * fees / 100;

                    installment.Value = installmentsFeesAmount * feesValue + value;
                    installment.ValuePerInstallment = installment.Value / installment.Number;
                    installment.Fees = true;
                }
                else
                {
                    installment.Value = value;
                    installment.ValuePerInstallment = installment.Value / installment.Number;
                    installment.Fees = false;
                }

                list.Add(installment);
            }

            return list;
        }


        public Transaction GetTransaction(string transactionId)
        {
            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");

            return PagarMeService.GetDefaultService().Transactions.Find(transactionId);
        }


        public Transaction EstornoCreditCard(string transactionId)
        {
            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");

            var transaction = PagarMeService.GetDefaultService().Transactions.Find(transactionId);

            transaction.Refund();

            return transaction;
        }


        public Transaction EstornoBoletoBancario(string transactionId, CancelDataBoleto boleto)
        {
            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");

            var transaction = PagarMeService.GetDefaultService().Transactions.Find(transactionId);

            var bankAccount = _mapper.Map<CancelDataBoleto, BankAccount>(boleto);

            transaction.Refund(bankAccount);

            return transaction;
        }
    }
}
