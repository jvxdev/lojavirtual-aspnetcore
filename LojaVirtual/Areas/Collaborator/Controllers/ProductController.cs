using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private ICategoryRepository _categoryRepository;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }


        public IActionResult Index(int? Page, string Search)
        {
            var products = _productRepository.ReadAll(Page, Search);
            return View(products);
        }


        public IActionResult Register()
        {
            ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Create(product);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            Product product = _productRepository.Read(Id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Update([FromForm] Product product, int Id)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);

                TempData["MSG_S"] = Message.MSG_S003;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }

        [HttpGet]
        [HttpReferer]
        public IActionResult Delete(int Id)
        {
            _productRepository.Delete(Id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}