using LojaVirtual.Models;
using LojaVirtual.Models.ViewModel.Components;
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
        private ICategoryRepository _categoryRepository;

        public ProductListViewComponent (IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? Page = 1;
            string Search = "";
            string Ordination = "Oa";
            IEnumerable<Category> categories = null;

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

            if (ViewContext.RouteData.Values.ContainsKey("slug"))
            {
                string slug = ViewContext.RouteData.Values["slug"].ToString();

                Category MainCategory = _categoryRepository.Read(slug);
                categories = _categoryRepository.ReadRecursiveCategories(MainCategory);
            }

            var viewModel = new ProductListViewModel() { productList = _productRepository.ReadAll(Page, Search, Ordination, categories) };
            return View(viewModel);
        }
    }
}
