using LojaVirtual.Libraries.Email;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Models.ViewModel;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        private IClientRepository _clientRepository;
        private INewsletterRepository _newsletterRepository;
        private ClientLogin _clientLogin;
        private EmailManage _emailManage;


        public HomeController(IProductRepository productRepository, IClientRepository clientRepository, INewsletterRepository newsletterRepository, ClientLogin clientLogin, EmailManage emailManage)
        {
            _productRepository = productRepository;
            _clientRepository = clientRepository;
            _newsletterRepository = newsletterRepository;
            _clientLogin = clientLogin;
            _emailManage = emailManage;
        }


        [HttpGet]
        public IActionResult Index(int? Page, string Search, string Ordination)
        {
            var viewModel = new IndexViewModel() { productList = _productRepository.ReadAll(Page, Search, Ordination) };
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletter, int? Page, string Search, string Ordination)
        {

            if (ModelState.IsValid)
            {
                _newsletterRepository.Create(newsletter);

                TempData["MSG_S"] = "Seu e-mail foi cadastrado com sucesso! Em breve você receberá novas promoções!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var viewModel = new IndexViewModel() { productList = _productRepository.ReadAll(Page, Search, Ordination) };
                return View(viewModel);
            }
        }


        public IActionResult Contact()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            Client client = _clientLogin.getClient();

            if (client == null)
            {
                return View();
            }
            else
            {
                return new ContentResult() { Content = "Você já está logado!" };
            }
        }


        [HttpPost]
        public IActionResult Login([FromForm] Client client)
        {
            Client clientDB = _clientRepository.Login(client.Email, client.Password);

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
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register([FromForm] Client client)
        {
            if (ModelState.IsValid)
            {
                _clientRepository.Create(client);

                TempData["MSG_S"] = "Cadastro realizado com sucesso! Entre com a sua nova conta!";

                return RedirectToAction(nameof(Login));
            }
            return View();
        }


        public IActionResult ShoppingKart()
        {
            return View();
        }


        public IActionResult ContactPost()
        {
            try
            {
                Contact contact = new Contact();

                contact.Name = HttpContext.Request.Form["name"];
                contact.Email = HttpContext.Request.Form["email"];
                contact.Menssage = HttpContext.Request.Form["text"];

                var showMensage = new List<ValidationResult>();
                var contexto = new ValidationContext(contact);
                bool isValid = Validator.TryValidateObject(contact, contexto, showMensage, true);

                if (isValid)
                {
                    _emailManage.NewEmail(contact);

                    ViewData["MSG_S"] = "Mensagem enviada com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var texto in showMensage)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contact;
                }

            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Algo de errado aconteceu... Tente novamente mais tarde!";
            }
            return View("Contact");
        }
    }
}
