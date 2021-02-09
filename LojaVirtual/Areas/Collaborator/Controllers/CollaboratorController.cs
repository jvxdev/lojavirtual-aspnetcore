using LojaVirtual.Libraries.Lang;
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
    public class CollaboratorController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;


        public CollaboratorController(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }


        [HttpGet]
        public IActionResult Index(int? page)
        {
            IPagedList<Models.Collaborator> collaborators = _collaboratorRepository.ReadAll(page);

            return View(collaborators);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Models.Collaborator collaborator)
        {
            if (ModelState.IsValid)
            {
                collaborator.Position = "C";
                _collaboratorRepository.Create(collaborator);

                TempData["MSG_S"] = Menssage.MSG_S001;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult Update(int Id)
        {
            var categoria = _collaboratorRepository.Read(Id);
            return View(categoria);
        }


        [HttpPost]
        public IActionResult Update([FromForm] Models.Collaborator collaborator, int Id)
        {
            if (ModelState.IsValid)
            {
                _collaboratorRepository.Update(collaborator);

                TempData["MSG_S"] = Menssage.MSG_S003;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _collaboratorRepository.Delete(Id);

            TempData["MSG_S"] = Menssage.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}
