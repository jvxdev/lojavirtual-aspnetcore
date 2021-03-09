using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Models.ViewModel
{
    public class IndexViewModel
    {
        public NewsletterEmail newsletterEmail { get; set; }

        public IPagedList<Product> productList { get; set; }
    }
}
