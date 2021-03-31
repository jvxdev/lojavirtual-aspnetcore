using AutoMapper;
using LojaVirtual.Controllers.Base;
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
        public PaymentController(IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieValorPrazoFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage) : base(productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
        }


        public IActionResult Index()
        {
            List<ProductItem> productKartItemFull = ReadProductDB();

            return View(productKartItemFull);
        }
    }
}
