using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Client.Controllers
{
    [ClientAuthorization]
    public class DeliveryAddressController : Controller
    {
        private ClientLogin _clientLogin;
        private IDeliveryAddressRepository _deliveryAddressRepository;


        public DeliveryAddressController(ClientLogin clientLogin, IDeliveryAddressRepository deliveryAddressRepository)
        {
            _clientLogin = clientLogin;
            _deliveryAddressRepository = deliveryAddressRepository;
        }


        public IActionResult Index()
        {
            var client = _clientLogin.GetClient();

            ViewBag.Client = client;
            ViewBag.DeliveryAddress = _deliveryAddressRepository.ReadAll(client.Id);

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] DeliveryAddress deliveryAddress, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                deliveryAddress.ClientId = _clientLogin.GetClient().Id;

                _deliveryAddressRepository.Create(deliveryAddress);

                if (returnUrl == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Update([FromForm] DeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                deliveryAddress.ClientId = _clientLogin.GetClient().Id;

                _deliveryAddressRepository.Update(deliveryAddress);

                TempData["MSG_S"] = Message.MSG_S003;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Models.Client client = _clientLogin.GetClient();

            DeliveryAddress deliveryAddress = _deliveryAddressRepository.Read(Id);

            if (client.Id == deliveryAddress.ClientId)
            {
                _deliveryAddressRepository.Delete(Id);

                TempData["MSG_S"] = Message.MSG_S002;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new ContentResult() { Content = "Acesso negado." };
            }
        }
    }
}
