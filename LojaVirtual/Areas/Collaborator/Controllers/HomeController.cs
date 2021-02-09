using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    public class HomeController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;
        private CollaboratorLogin _collaboratorLogin;


        public HomeController(ICollaboratorRepository collaboratorRepository, CollaboratorLogin collaboratorLogin)
        {
            _collaboratorRepository = collaboratorRepository;
            _collaboratorLogin = collaboratorLogin;
        }


        [Area("Collaborator")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [Area("Collaborator")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [Area("Collaborator")]
        [HttpPost]
        public IActionResult Register([FromForm] Models.Collaborator collaborator)
        {
            if (ModelState.IsValid)
            {
                _collaboratorRepository.Create(collaborator);

                TempData["MSG_S"] = "Cadastro realizado com sucesso! Entre com a sua nova conta!";

                return RedirectToAction(nameof(Login));
            }
            return View();
        }


        [Area("Collaborator")]
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


        [Area("Collaborator")]
        [CollaboratorAuthorization]
        public IActionResult Logout()
        {
            _collaboratorLogin.Logout();

            return RedirectToAction("Login", "Home");
        }


        [Area("Collaborator")]
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
