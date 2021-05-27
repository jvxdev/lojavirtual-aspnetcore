using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
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
    }
}
