using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Order
    {
        public int Id { get; set; }


        [ForeignKey("Client")]
        public int? ClientId { get; set; }


        public string TransactionId { get; set; }


        public string TransactionData { get; set; }


        public string FreteCompany { get; set; }


        public string FreteTrackingCod { get; set; }


        public string PaymentForm { get; set; }


        public decimal TotalValue { get; set; }


        public string ProductsData { get; set; }


        public DateTime RegistryDate { get; set; }


        public string Situation { get; set; }


        //Nota fiscal eletrônica
        public string NFE { get; set; }


        public virtual Client Client { get; set; }


        [ForeignKey("OrderId")]
        public virtual ICollection<OrderSituation> OrderSituations { get; set; }
    }
}
