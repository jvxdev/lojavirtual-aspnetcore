using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Colaborator.Controllers
{
    public class HomeController : Controller
    {
        private IColaboratorRepository _colaboratorRepository;
        private ColaboratorLogin _colaboratorLogin;


        public HomeController(IColaboratorRepository colaboratorRepository, ColaboratorLogin colaboratorLogin)
        {
            _colaboratorRepository = colaboratorRepository;
            _colaboratorLogin = colaboratorLogin;
        }


        [Area("Colaborator")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Area("Colaborator")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Area("Colaborator")]
        [HttpPost]
        public IActionResult Register([FromForm] Models.Colaborator colaborator)
        {
            if (ModelState.IsValid)
            {
                _colaboratorRepository.Create(colaborator);

                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                return RedirectToAction(nameof(Register));
            }
            return View();
        }


        [Area("Colaborator")]
        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborator colaborator)
        {
            Models.Colaborator colaboratorDB = _colaboratorRepository.Login(colaborator.Email, colaborator.Password);

            if (colaboratorDB != null)
            {
                _colaboratorLogin.Login(colaboratorDB);

                return new RedirectResult(Url.Action(nameof(Panel)));
            }
            else
            {
                TempData["MSG_E"] = "E-mail ou senha inválidos!";

                return View();
            }
        }

        [Area("Colaborator")]
        [ColaboratorAuthorization]
        public IActionResult Logout()
        {
            _colaboratorLogin.Logout();

            return RedirectToAction("Login", "Home");
        }


        [Area("Colaborator")]
        [ColaboratorAuthorization]
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
