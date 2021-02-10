using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Text;
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
        private EmailManage _emailManage;


        public CollaboratorController(ICollaboratorRepository collaboratorRepository, EmailManage emailManage)
        {
            _collaboratorRepository = collaboratorRepository;
            _emailManage = emailManage;
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
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                collaborator.Position = "C";
                collaborator.Password = KeyGenerator.GetUniqueKey(8);
                _collaboratorRepository.Create(collaborator);

                TempData["MSG_S"] = Message.MSG_S001;

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
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                _collaboratorRepository.Update(collaborator);

                TempData["MSG_S"] = Message.MSG_S003;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult NewPasswordGenerator(int Id)
        {
            Models.Collaborator collaborator =_collaboratorRepository.Read(Id);
            collaborator.Password = KeyGenerator.GetUniqueKey(8);
            _collaboratorRepository.UpdatePassword(collaborator);

            _emailManage.NewPasswordEmail(collaborator);

            TempData["MSG_S"] = Message.MSG_S004;

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            _collaboratorRepository.Delete(Id);

            TempData["MSG_S"] = Message.MSG_S002;

            return RedirectToAction(nameof(Index));
        }
    }
}
