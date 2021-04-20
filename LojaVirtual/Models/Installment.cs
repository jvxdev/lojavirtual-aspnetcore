using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Installment
    {
        public int Number { get; set; }


        public decimal Value { get; set; }


        public decimal ValuePerInstallment { get; set; }


        public bool Fees { get; set; }
    }
}
