using LojaVirtual.Models.ProductAggregator;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Models.ViewModel.Components
{
    public class ProductListViewModel
    {
        public IPagedList<Product> productList { get; set; }


        public List<SelectListItem> ordinationList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ordem alfabética", "Oa"),
                    new SelectListItem("Menor preço", "Mep"),
                    new SelectListItem("Maior preço", "Map")
                };
            }
            private set { }
        }
    }
}
