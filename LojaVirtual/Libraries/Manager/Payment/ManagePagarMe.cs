using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using Microsoft.Extensions.Configuration;
using PagarMe;

namespace LojaVirtual.Libraries.Manager.Payment
{
    public class ManagePagarMe
    {
        private IConfiguration _conf;
        private ClientLogin _clientLogin;


        public ManagePagarMe(IConfiguration conf, ClientLogin clientLogin)
        {
            _conf = conf;
            _clientLogin = clientLogin;
        }

        
        public Transaction GerarBoleto(decimal value)
        {
                Client client = _clientLogin.getClient();

                PagarMeService.DefaultApiKey = _conf.GetValue<String>("Payment:PagarMe:ApiKey");
                PagarMeService.DefaultEncryptionKey = _conf.GetValue<String>("Payment:PagarMe:EcryptionKey");

                Transaction transaction = new Transaction();

                transaction.Amount = Convert.ToInt32(value);
                transaction.PaymentMethod = PaymentMethod.Boleto;

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

                transaction.Save();

                return transaction;
        }


        public Transaction GerarPagCartaoCredito(CreditCard creditCard, Installment installment, DeliveryAddress deliveryAddress, ValorPrazoFrete valorFrete, List<ProductItem> products)
        {
            Client client = _clientLogin.getClient();

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

            var fee = Convert.ToDecimal(valorFrete.Valor);

            transaction.Shipping = new PagarMe.Shipping
            {
                Name = deliveryAddress.AddressName,
                Fee = Mask.ConvertValuePagarMe(fee),
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
    }
}
