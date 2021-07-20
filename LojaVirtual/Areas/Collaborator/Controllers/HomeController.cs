using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Security;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class HomeController : Controller
    {
        private ICollaboratorRepository _collaboratorRepository;
        private CollaboratorLogin _collaboratorLogin;
        private IClientRepository _clientRepository;
        private IProductRepository _productRepository;
        private INewsletterRepository _newsletterRepository;
        private IOrderRepository _orderRepository;
        private EmailManage _emailManage;


        public HomeController
            (
            ICollaboratorRepository collaboratorRepository,
            CollaboratorLogin collaboratorLogin,
            IClientRepository clientRepository,
            IProductRepository productRepository,
            INewsletterRepository newsletterRepository,
            IOrderRepository orderRepository,
            EmailManage emailManage
            )
        {
            _collaboratorRepository = collaboratorRepository;
            _collaboratorLogin = collaboratorLogin;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _newsletterRepository = newsletterRepository;
            _orderRepository = orderRepository;
            _emailManage = emailManage;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Models.Collaborator collaborator)
        {
            if (ModelState.IsValid)
            {
                _collaboratorRepository.Create(collaborator);

                TempData["MSG_S"] = Message.MSG_S005;

                return RedirectToAction(nameof(Login));
            }
            return View();
        }


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


        [CollaboratorAuthorization]
        [HttpReferer]
        public IActionResult Logout()
        {
            _collaboratorLogin.Logout();

            return RedirectToAction("Login", "Home");
        }


        [HttpGet]
        public IActionResult RecoverPassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult RecoverPassword([FromForm] Models.Collaborator collaborator)
        {
            var databaseCollaborator = _collaboratorRepository.GetCollaboratorByEmail(collaborator.Email);

            if (databaseCollaborator != null && databaseCollaborator.Count > 0)
            {
                string idCrypt = Base64Crypt.Base64Encode(databaseCollaborator.First().Id.ToString());

                _emailManage.RecoverPasswordEmail(databaseCollaborator.First(), idCrypt);

                TempData["MSG_S"] = Message.MSG_S011;

                ModelState.Clear();
            }
            else
            {
                TempData["MSG_E"] = Message.MSG_E016;
            }

            return View();
        }


        [HttpGet]
        public IActionResult CreateNewPassword(string id)
        {
            try
            {
                var idCollaboratorDecrypt = Base64Crypt.Base64Decode(id);
                int idCollaborator;

                if (!int.TryParse(idCollaboratorDecrypt, out idCollaborator))
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
        public IActionResult CreateNewPassword([FromForm] Models.Collaborator collaborator, string id)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Email");

            if (ModelState.IsValid)
            {
                int idCollaborator;

                try
                {
                    var idCollaboratorDecrypt = Base64Crypt.Base64Decode(id);

                    if (!int.TryParse(idCollaboratorDecrypt, out idCollaborator))
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

                var collaboratorDB = _collaboratorRepository.Read(idCollaborator);

                if (collaboratorDB != null)
                {
                    collaboratorDB.Password = collaborator.Password;

                    _collaboratorRepository.UpdatePassword(collaboratorDB);

                    TempData["MSG_S"] = Message.MSG_S010;

                    return RedirectToAction(nameof(Login));
                }
            }

            return View();
        }


        [CollaboratorAuthorization]
        public IActionResult Panel()
        {
            ViewBag.Clients = _clientRepository.TotalClients();
            ViewBag.Newsletter = _newsletterRepository.TotalNewsletters();
            ViewBag.Products = _productRepository.TotalProducts();
            ViewBag.TotalOrders = _orderRepository.TotalOrders();
            ViewBag.TotalValueOrders = _orderRepository.TotalValueOrders();
            ViewBag.TotalOrdersCreditCard = _orderRepository.TotalOrdersCreditCard();
            ViewBag.TotalOrdersBoletoBancario = _orderRepository.TotalOrdersBoletoBancario();

            return View();
        }


        public IActionResult NewsletterCSV()
        {
            var news = _newsletterRepository.ReadAll();

            StringBuilder sb = new StringBuilder();

            foreach (var email in news)
            {
                sb.AppendLine(email.Email);
            }

            byte[] buffer = Encoding.ASCII.GetBytes(sb.ToString());

            return File(buffer, "text/csv", $"newsletter.csv");
        }
    }
}
