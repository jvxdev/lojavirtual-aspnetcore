using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Client.Controllers
{
    [Area("Client")]
    public class ClientController : Controller
    {
        private ClientLogin _clientLogin;
        private IClientRepository _clientRepository;


        public ClientController(ClientLogin clientLogin, IClientRepository clientRepository)
        {
            _clientLogin = clientLogin;
            _clientRepository = clientRepository;
        }


        [ClientAuthorization]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Models.Client client, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                client.Situation = SituationConst.Active;

                _clientRepository.Create(client);
                _clientLogin.Login(client);

                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }

            return View();
        }


        [ClientAuthorization]
        [HttpGet]
        public IActionResult Update()
        {
            Models.Client client =_clientRepository.Read(_clientLogin.GetClient().Id);

            return View(client);
        }


        [ClientAuthorization]
        [HttpPost]
        public IActionResult Update(Models.Client client)
        {
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirmation");

            if (ModelState.IsValid)
            {
                client.Password = _clientLogin.GetClient().Password;
                _clientRepository.Update(client);

                _clientLogin.Login(client);

                TempData["MSG_S"] = Message.MSG_S006;

                return RedirectToAction(nameof(Update));
            }

            return View();
        }


        [ClientAuthorization]
        [HttpGet]
        public IActionResult UpdatePassword()
        {
            Models.Client client = _clientRepository.Read(_clientLogin.GetClient().Id);

            return View(client);
        }


        [ClientAuthorization]
        [HttpPost]
        public IActionResult UpdatePassword(Models.Client client)
        {
            ModelState.Remove("Name");
            ModelState.Remove("BirthDate");
            ModelState.Remove("Sex");
            ModelState.Remove("CPF");
            ModelState.Remove("Phone");
            ModelState.Remove("CEP");
            ModelState.Remove("State");
            ModelState.Remove("City");
            ModelState.Remove("Neighborhood");
            ModelState.Remove("Street");
            ModelState.Remove("HouseNumber");
            ModelState.Remove("Complement");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                Models.Client clientDB = _clientRepository.Read(_clientLogin.GetClient().Id);
                clientDB.Password = client.Password;
                
                _clientRepository.Update(clientDB);

                _clientLogin.Login(clientDB);

                TempData["MSG_S"] = Message.MSG_S006;

                return RedirectToAction(nameof(UpdatePassword));
            }

            return View();
        }
    }
}
