using LojaVirtual.Models.ProductAggregator;
using PagarMe;
using System.Collections.Generic;

namespace LojaVirtual.Models
{
    public class ProductTransaction
    {
        public Transaction Transaction { get; set; }
        public List<ProductItem> Products { get; set; }
    }
}
