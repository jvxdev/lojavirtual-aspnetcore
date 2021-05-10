using System.Collections.Generic;

namespace LojaVirtual.Models
{
    public class Frete
    {
        public int CEP { get; set; }


        public string codShoppingKart { get; set; }


        public List<ValorPrazoFrete> valuesList { get; set; }
    }
}
