using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
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


        public HomeController
            (
            ICollaboratorRepository collaboratorRepository, 
            CollaboratorLogin collaboratorLogin,
            IClientRepository clientRepository,
            IProductRepository productRepository,
            INewsletterRepository newsletterRepository,
            IOrderRepository orderRepository
            )
        {
            _collaboratorRepository = collaboratorRepository;
            _collaboratorLogin = collaboratorLogin;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
            _newsletterRepository = newsletterRepository;
            _orderRepository = orderRepository;
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




        [HttpPost]
        public IActionResult PasswordRecover()
        {
            return View();
        }


        public IActionResult NewPassword()
        {
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
