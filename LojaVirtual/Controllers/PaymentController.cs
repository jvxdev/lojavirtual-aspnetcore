﻿using AutoMapper;
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

namespace LojaVirtual.Controllers
{
    public class PaymentController : BaseController
    {
        private Cookie _cookie;


        public PaymentController(IDeliveryAddressRepository deliveryAddressRepository, IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage, Cookie cookie) : base(deliveryAddressRepository, productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
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
                var deliveryAdress = _deliveryAddressRepository.Read(deliveryAddressId);

                var ShoppingKartHash = HashGenerator(_cookieShoppingKart.Read());
                var cep = deliveryAdress.CEP;
                
                _cookieFrete.Read()

                var Frete = .Where(a => a.TipoFrete == tipoFreteSelected).FirstOrDefault();

                if (Frete != null)
                {
                    ViewBag.Frete = Frete;
                    List<ProductItem> productKartItemFull = ReadProductDB();

                    return View(productKartItemFull);
                }
            }

            TempData["MSG_E"] = Message.MSG_E010;
            return RedirectToAction("Index", "ShoppingKart");
        }
    }
}