using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;


        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
            _categoryRepository.Delete(id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}
