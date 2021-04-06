using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Login;
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
                        Number = client.CPF
                    }
                },
                    PhoneNumbers = new string[]
                    {
                    "+55" + client.Phone
                    },
                    Birthday = client.BirthDate.ToString("yyyy-MM-dd")
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
    }
}
