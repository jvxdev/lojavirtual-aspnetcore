using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class ProductController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;


        public ProductController(ICategoryRepository categoryRepository, IProductRepository productController)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productController;
        }


        [HttpGet]
        [Route("Product/Category/{slug}")]
        public IActionResult CategoryList (string slug)
        {
            return View(_categoryRepository.Read(slug));
        }


        public IActionResult Show(int Id)
        {
            return View(_productRepository.Read(Id));
        }
    }
}
