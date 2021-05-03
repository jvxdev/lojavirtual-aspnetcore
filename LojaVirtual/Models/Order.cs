using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Order
    {
        public int Id { get; set; }


        public int? ClientId { get; set; }


        public string TransactionId { get; set; }


        public string TransactionData { get; set; }


        public string FreteCompany { get; set; }


        public string FreteTrackingCod { get; set; }


        public string PaymentForm { get; set; }


        public decimal TotalValue { get; set; }


        public string ProductsData { get; set; }


        public DateTime RegistryData { get; set; }


        public string Situation { get; set; }


        public string NFE { get; set; }
    }
}
