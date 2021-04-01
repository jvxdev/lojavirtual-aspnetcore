using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Repositories.Contracts;
using LojaVirtual.Libraries.Login;

namespace LojaVirtual.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        IClientRepository _clientRepository;
        ClientLogin _clientLogin;


        public HomeController(IClientRepository clientRepository, ClientLogin clientLogin)
        {
            _clientRepository = clientRepository;
            _clientLogin = clientLogin;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login([FromForm] Models.Client client)
        {
            Models.Client clientDB = _clientRepository.Login(client.Email, client.Password);

            if (clientDB != null)
            {
                _clientLogin.Login(clientDB);

                return new RedirectResult(Url.Action(nameof(Panel)));
            }
            else
            {
                TempData["MSG_E"] = "E-mail ou senha inválidos!";

                return View();
            }
        }


        [HttpGet]
        [ClientAuthorization]
        public IActionResult Panel()
        {
            return new ContentResult() { Content = "Olá! Você está no Painel do Cliente" };
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Models.Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Create(client);

                TempData["MSG_S"] = "Cadastro realizado com sucesso! Entre com a sua nova conta!";

                return RedirectToAction(nameof(Login));
            }
            return View();
        }
    }
}
