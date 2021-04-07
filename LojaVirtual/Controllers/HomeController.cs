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
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletter)
        {

            if (ModelState.IsValid)
            {
                _newsletterRepository.Create(newsletter);

                TempData["MSG_S"] = "Seu e-mail foi cadastrado com sucesso! Em breve você receberá novas promoções!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }


        public IActionResult Contact()
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
