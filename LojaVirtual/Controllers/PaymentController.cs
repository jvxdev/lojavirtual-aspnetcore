using AutoMapper;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Libraries.Login;

namespace LojaVirtual.Controllers
{
    public class PaymentController : BaseController
    {
        private Cookie _cookie;


        public PaymentController(ClientLogin clientLogin, IDeliveryAddressRepository deliveryAddressRepository, IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage, Cookie cookie) : base(clientLogin, deliveryAddressRepository, productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
            _cookie = cookie;
        }


        [HttpGet]
        [ClientAuthorization]
        public IActionResult Index()
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            if (tipoFreteSelected != null)
            {
                var deliveryAddress = GetAddress();

                var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

                int cep = int.Parse(Mask.Delete(deliveryAddress.CEP));

                ViewBag.Frete = GetFrete(cep.ToString());
                List<ProductItem> productKartItemFull = ReadProductDB();
                ViewBag.Products = productKartItemFull;

                return View("Index");
            }

            TempData["MSG_E"] = Message.MSG_E010;
            return RedirectToAction("DeliveryAddress", "ShoppingKart");
        }


        [HttpPost]
        [ClientAuthorization]
        public IActionResult Index([FromForm] CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                DeliveryAddress deliveryAddress = GetAddress();
                ValorPrazoFrete frete = GetFrete(deliveryAddress.CEP.ToString());
                List<ProductItem> products = ReadProductDB();

                return View();
            }
            else
            {
                return Index();
            }
        }


        private DeliveryAddress GetAddress()
        {
            DeliveryAddress deliveryAddress = null;

            var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));


            if (deliveryAddressId == 0)
            {
                Client client = _clientLogin.getClient();

                deliveryAddress = new DeliveryAddress();
                deliveryAddress.AddressName = "Endereço do cliente";
                deliveryAddress.Id = 0;
                deliveryAddress.State = client.State;
                deliveryAddress.City = client.City;
                deliveryAddress.Neighborhood = client.Neighborhood;
                deliveryAddress.Street = client.Street;
                deliveryAddress.HouseNumber = client.HouseNumber;
            }
            else
            {
                var address = _deliveryAddressRepository.Read(deliveryAddressId);
            }

            return deliveryAddress;
        }


        private ValorPrazoFrete GetFrete(string cepDestino)
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

            int cep = int.Parse(Mask.Delete(cepDestino));

            Frete frete = _cookieFrete.Read().Where(a => a.CEP == cep && a.codShoppingKart == shoppingKartHash).FirstOrDefault();

            if (frete != null)
            {
                return frete.valuesList.Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}