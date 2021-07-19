using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Security;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private IClientRepository _clientRepository;
        private IDeliveryAddressRepository _deliveryAddressRepository;
        private ClientLogin _clientLogin;
        private EmailManage _emailManage;
        private string passPhrase = "xdoskx21321Oo@@@sko443askzmkas12313";


        public HomeController(IClientRepository clientRepository, IDeliveryAddressRepository deliveryAddressRepository, ClientLogin clientLogin, EmailManage emailManage)
        {
            _clientRepository = clientRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _clientLogin = clientLogin;
            _emailManage = emailManage;
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
                    string idCrypt = Base64Crypt.Base64Encode(databaseClient.Id.ToString());

                    _emailManage.RecoverPasswordEmail(databaseClient, idCrypt);

                    TempData["MSG_S"] = Message.MSG_S011;

                    ModelState.Clear();
                }
                else
                {
                    TempData["MSG_E"] = Message.MSG_E016;
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult CreateNewPassword(string id)
        {
            try
            {
                var idClientDecrypt = Base64Crypt.Base64Decode(id);
                int idClient;

                if (!int.TryParse(idClientDecrypt, out idClient))
                {
                    TempData["MSG_E"] = Message.MSG_E017;
                }
            }
            catch (System.FormatException e)
            {
                TempData["MSG_E"] = Message.MSG_E017;
            }

            return View();
        }


        [HttpPost]
        public IActionResult CreateNewPassword([FromForm] Models.Client client, string id)
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
                int idClient;

                try
                {
                    var idClientDecrypt = Base64Crypt.Base64Decode(id);

                    if (!int.TryParse(idClientDecrypt, out idClient))
                    {
                        TempData["MSG_E"] = Message.MSG_E017;

                        return View();
                    }
                }
                catch (System.FormatException e)
                {
                    TempData["MSG_E"] = Message.MSG_E017;

                    return View();
                }

                var clientDB = _clientRepository.Read(idClient);

                if (clientDB != null)
                {
                    clientDB.Password = client.Password;

                    _clientRepository.Update(clientDB);

                    TempData["MSG_S"] = Message.MSG_S010;

                    return RedirectToAction(nameof(Login));
                }
            }

            return View();
        }
    }
}
