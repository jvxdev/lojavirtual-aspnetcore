using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }

        public string CardHolderName { get; set; }

        public string CardExpirationDate { get; set; }

        public string CardCvv { get; set; }
    }
}
