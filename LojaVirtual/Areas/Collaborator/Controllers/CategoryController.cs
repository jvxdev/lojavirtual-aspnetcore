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
            if(ModelState.IsValid)
            {
                _categoryRepository.Create(category);

                TempData["MSG_S"] = Message.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            var categoria = _categoryRepository.Read(Id);
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View(categoria);
        }


        [HttpPost]
        public IActionResult Update([FromForm] Category category, int Id)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);

                TempData["MSG_S"] = Message.MSG_S003;

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.ReadAll().Where(a => a.Id != Id).Select(a => new SelectListItem(a.Name, a.Id.ToString()));
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _categoryRepository.Delete(Id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}
