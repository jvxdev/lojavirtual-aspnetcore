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
                    return RedirectToAction("Index", "Home", new {  area = "" });
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
        public IActionResult Logout()
        {
            _clientLogin.Logout();

            return RedirectToAction("Index", "Home", new { area = "" });
        }


        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RecoverPassword([FromForm] Models.Client client)
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
            ModelState.Remove("Password");
            ModelState.Remove("PasswordConfirmation");

            if (ModelState.IsValid)
            {
                var databaseClient = _clientRepository.GetClientByEmail(client.Email);

                if (databaseClient != null)
                {

                }
                else
                {
                    TempData["MSG_E"] = Message.MSG_E016;
                
                    return View();
                }
            }
        }
    }
}
