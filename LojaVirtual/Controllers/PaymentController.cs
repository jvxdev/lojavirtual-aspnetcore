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


        [ClientAuthorization]
        public IActionResult Index()
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            if (tipoFreteSelected != null)
            {
                var deliveryAddressId = int.Parse(_cookie.Read("ShoppingKart.DeliveryAddress", false).Replace("-end", ""));

                int cep = 0;

                if (deliveryAddressId == 0)
                {
                    cep = int.Parse(Mask.Delete(_clientLogin.getClient().CEP));
                }
                else
                {
                    var deliveryAdress = _deliveryAddressRepository.Read(deliveryAddressId);
                    cep = int.Parse(Mask.Delete(deliveryAdress.CEP));
                }

                var shoppingKartHash = HashGenerator(_cookieShoppingKart.Read());

                Frete frete = _cookieFrete.Read().Where(a => a.CEP == cep && a.codShoppingKart == shoppingKartHash).FirstOrDefault();

                if (frete != null)
                {
                    ViewBag.Frete = frete.valuesList.Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();
                    List<ProductItem> productKartItemFull = ReadProductDB();

                    return View(productKartItemFull);
                }
            }

            TempData["MSG_E"] = Message.MSG_E010;
            return RedirectToAction("Index", "ShoppingKart");
        }
    }
}