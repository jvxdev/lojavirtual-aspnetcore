using AutoMapper;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class PaymentController : BaseController
    {
        private Cookie _cookie;


        public PaymentController(IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieValorPrazoFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage, Cookie cookie) : base(productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
            _cookie = cookie;
        }


        public IActionResult Index()
        {
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);

            if (tipoFreteSelected != null)
            {
                var Frete = _cookieValorPrazoFrete.Read().Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();

                if (Frete != null)
                {
                    ViewBag.Frete = Frete;
                    List<ProductItem> productKartItemFull = ReadProductDB();

                    return View(productKartItemFull);
                }
                    TempData["MSG_E"] = Message.MSG_E010;
                    return RedirectToAction("Index", "ShoppingKart");
            }

            return null;
        }
    }
}