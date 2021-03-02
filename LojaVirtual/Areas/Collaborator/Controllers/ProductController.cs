using LojaVirtual.Libraries.Files;
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
        private IImageRepository _imageRepository;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
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

                List<Image> defImagesList = ImageManage.MoveProductImage(new List<string>(Request.Form["image"]), product.Id);

                _imageRepository.ImagesUpload(defImagesList, product.Id);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
                
                product.Images = new List<string>(Request.Form["image"]).Where(a => a.Trim().Length > 0).Select(a => new Image() { Path = a }).ToList();

                return View(product);
            }
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            Product product = _productRepository.Read(Id);
            return View(product);
        }


        [HttpPost]
        public IActionResult Update(Product product, int Id)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);

                List<Image> defImagesList = ImageManage.MoveProductImage(new List<string>(Request.Form["image"]), product.Id);

                _imageRepository.DeleteAllProductImages(product.Id);
                _imageRepository.ImagesUpload(defImagesList, product.Id);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));

                product.Images = new List<string>(Request.Form["image"]).Where(a => a.Trim().Length > 0).Select(a => new Image() { Path = a }).ToList();

                return View(product);
            }
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