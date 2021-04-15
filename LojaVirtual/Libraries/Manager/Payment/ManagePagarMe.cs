using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
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


        public object GerarBoleto(decimal Value)
        {
            try
            {
                Client client = _clientLogin.getClient();

                PagarMeService.DefaultApiKey = _conf.GetValue<String>("Pagamento:PagarMe:ApiKey");
                PagarMeService.DefaultEncryptionKey = _conf.GetValue<String>("Pagamento:PagarMe:EcryptionKey");

                Transaction transaction = new Transaction();

                transaction.Amount = Convert.ToInt32(Value);
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

                return new
                {
                    BoletoUrl = transaction.BoletoUrl,
                    BarCode = transaction.BoletoBarcode,
                    BoletoExpirationDate = transaction.BoletoExpirationDate

                };
            }
            catch (Exception e)
            {
                return new { Erro = e.Message };
            }
        }


        public object GerarPagCartaoCredito(CreditCard creditCard, DeliveryAddress deliveryAddress, ValorPrazoFrete valorFrete)
        {
            Client client = _clientLogin.getClient();

            PagarMeService.DefaultApiKey = _conf.GetValue<String>("Pagamento:PagarMe:ApiKey");
            PagarMeService.DefaultEncryptionKey = _conf.GetValue<String>("Pagamento:PagarMe:EcryptionKey");

            Card card = new Card();
            card.Number = creditCard.CardNumber;
            card.HolderName = creditCard.CardHolderName;
            card.ExpirationDate = creditCard.CardExpirationDate;
            card.Cvv = creditCard.CardCvv;

            card.Save();

            Transaction transaction = new Transaction();

            transaction.Amount = 2100;
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

            transaction.Shipping = new PagarMe.Shipping
            {
                Name = deliveryAddress.AddressName,
                Fee = Convert.ToInt32(valorFrete.Valor),
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

            transaction.Item = new[]
            {
              new Item()
              {
                Id = "1",
                Title = "Little Car",
                Quantity = 1,
                Tangible = true,
                UnitPrice = 1000
              },
              new Item()
              {
                Id = "2",
                Title = "Baby Crib",
                Quantity = 1,
                Tangible = true,
                UnitPrice = 1000
              }
            };

            transaction.Save();
        }
    }
}
