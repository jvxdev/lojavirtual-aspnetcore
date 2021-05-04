using LojaVirtual.Models.ProductAggregator;
using PagarMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Models
{
    public class ProductTransaction
    {
        public Transaction Transaction { get; set; }
        public List<ProductItem> Products { get; set; }
    }
}
