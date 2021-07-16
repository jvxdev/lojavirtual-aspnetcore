using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text;
using X.PagedList;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;


        public CategoryController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }


        public IActionResult Index(int? page)
        {
            var categories = _categoryRepository.ReadAll(page);
            return View(categories);
        }


        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var categoria = _categoryRepository.Read(id);
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View(categoria);
        }


        [HttpPost]
        public IActionResult Update([FromForm] Category category, int id)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);

                TempData["MSG_S"] = Message.MSG_S003;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpGet]
        [HttpReferer]
        public IActionResult Delete(int id)
        {
            var categoriesSon = _categoryRepository.GetCategoryByFatherCategory(id);

            if (categoriesSon.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in categoriesSon)
                {
                    sb.Append($"'{item.Name}' ");
                }

                TempData["MSG_E"] = string.Format(Message.MSG_E014, sb.ToString());

                return RedirectToAction(nameof(Index));
            }

            var productsSon = _productRepository.GetProductByCategory(id);

            if (productsSon.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in productsSon)
                {
                    sb.Append($"'{item.Name}' ");
                }

                TempData["MSG_E"] = string.Format(Message.MSG_E015, sb.ToString());

                return RedirectToAction(nameof(Index));
            }

            _categoryRepository.Delete(id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}
