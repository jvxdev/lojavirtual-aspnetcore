using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class HomeController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;
        private CollaboratorLogin _collaboratorLogin;


        public HomeController(ICollaboratorRepository collaboratorRepository, CollaboratorLogin collaboratorLogin)
        {
            _collaboratorRepository = collaboratorRepository;
            _collaboratorLogin = collaboratorLogin;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
                _collaboratorRepository.Create(collaborator);

                TempData["MSG_S"] = Message.MSG_S005;

                return RedirectToAction(nameof(Login));
            }
            return View();
        }


        [HttpPost]
        public IActionResult Login([FromForm] Models.Collaborator collaborator)
        {
            Models.Collaborator collaboratorDB = _collaboratorRepository.Login(collaborator.Email, collaborator.Password);

            if (collaboratorDB != null)
            {
                _collaboratorLogin.Login(collaboratorDB);

                return new RedirectResult(Url.Action(nameof(Panel)));
            }
            else
            {
                TempData["MSG_E"] = "E-mail ou senha inválidos!";

                return View();
            }
        }


        [CollaboratorAuthorization]
        [HttpReferer]
        public IActionResult Logout()
        {
            _collaboratorLogin.Logout();

            return RedirectToAction("Login", "Home");
        }


        [CollaboratorAuthorization]
        public IActionResult Panel()
        {
            return View();
        }


        [HttpPost]
        public IActionResult PasswordRecover()
        {
            return View();
        }


        public IActionResult NewPassword()
        {
            return View();
        }
    }
}
