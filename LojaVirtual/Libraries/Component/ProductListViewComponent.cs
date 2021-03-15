using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class ProductListViewComponent : ViewComponent
    {
        private IProductRepository _productRepository;

        public ProductListViewComponent (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? Page = 1;
            string Search = "";
            string Ordination = "oa";


            if (HttpContext.Request.Query.ContainsKey("Page"))
            {
                Page = int.Parse(HttpContext.Request.Query["Page"]);
            }

            if (HttpContext.Request.Query.ContainsKey("Search"))
            {
                Search = HttpContext.Request.Query["Search"].ToString();
            }

            if (HttpContext.Request.Query.ContainsKey("Ordination"))
            {
                Ordination = HttpContext.Request.Query["Ordination"].ToString();
            }

            var list = _productRepository.ReadAll(Page, Search, Ordination);
            return View();
        }
    }
}
