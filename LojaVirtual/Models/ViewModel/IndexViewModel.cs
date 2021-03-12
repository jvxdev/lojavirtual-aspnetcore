using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Models.ViewModel
{
    public class IndexViewModel
    {
        public NewsletterEmail newsletter { get; set; }


        public IPagedList<Product> productList { get; set; }


        public List<SelectListItem> ordinationList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ordem alfabética", "oa"),
                    new SelectListItem("Menor preço", "mep"),
                    new SelectListItem("Maior preço", "map")
                };
            }
            private set { }
        }
    }
}
