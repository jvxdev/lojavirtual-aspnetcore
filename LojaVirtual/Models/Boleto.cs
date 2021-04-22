using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Boleto
    {
        public string TransactionId { get; set; }


        public string BoletoURL { get; set; }


        public string BarCode { get; set; }


        public DateTime? BoletoExpirationDate { get; set; }


        public string Erro { get; set; }
    }
}
