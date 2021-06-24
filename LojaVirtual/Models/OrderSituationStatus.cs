using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class OrderSituationStatus
    {
        public string Situation { get; set; }


        public DateTime? Date { get; set; }


        public bool Concluded { get; set; }


        public string Color { get; set; }
    }
}
