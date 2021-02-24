using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        
        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public IActionResult Index(int? Page, string Search)
        {
            var products = _productRepository.ReadAll(Page, Search);
            return View(products);
        }
    }
}
