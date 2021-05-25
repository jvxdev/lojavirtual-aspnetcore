using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Client.Controllers
{
    public class ClientController : Controller
    {
        private ClientLogin _clientLogin;
        private ClientRepository _clientRepository;


        public ClientController(ClientLogin clientLogin, ClientRepository clientRepository)
        {
            _clientLogin = clientLogin;
            _clientRepository = clientRepository;
        }


        [ClientAuthorization]
        public IActionResult Index()
        {
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

                return RedirectToAction(nameof(Index));
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
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                Models.Client clientDB = _clientRepository.Read(_clientLogin.GetClient().Id);
                clientDB.Password = client.Password;
                
                _clientRepository.Update(clientDB);

                _clientLogin.Login(clientDB);

                TempData["MSG_S"] = Message.MSG_S006;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }
}
