using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class Frete
    {
        public int CEP { get; set; }


        public string codShoppingKart { get; set; }


        public List<ValorPrazoFrete> ValuesList { get; set; }
    }
}
