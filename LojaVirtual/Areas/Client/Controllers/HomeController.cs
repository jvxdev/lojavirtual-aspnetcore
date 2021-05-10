using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        IClientRepository _clientRepository;
        IDeliveryAddressRepository _deliveryAddressRepository;
        ClientLogin _clientLogin;



        public HomeController(IClientRepository clientRepository, IDeliveryAddressRepository deliveryAddressRepository, ClientLogin clientLogin)
        {
            _clientRepository = clientRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
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
        public IActionResult Login([FromForm] Models.Client client, string returnUrl = null)
        {
            Models.Client clientDB = _clientRepository.Login(client.Email, client.Password);

            if (clientDB != null)
            {
                _clientLogin.Login(clientDB);

                if (returnUrl == null)
                {
                    return new RedirectResult(Url.Action(nameof(Panel)));
                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }
            else
            {
                TempData["MSG_E"] = Message.MSG_E011;
                return View();
            }
        }


        [HttpGet]
        [ClientAuthorizationAttribute]
        public IActionResult Panel()
        {
            return new ContentResult() { Content = "Você está no Painel do Cliente" };
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


        [HttpGet]
        public IActionResult RegisterDeliveryAddress()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RegisterDeliveryAddress([FromForm] DeliveryAddress deliveryAddress, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                deliveryAddress.ClientId = _clientLogin.getClient().Id;

                _deliveryAddressRepository.Create(deliveryAddress);

                if (returnUrl == null)
                {

                }
                else
                {
                    return LocalRedirectPermanent(returnUrl);
                }
            }

            return View();
        }
    }
}
