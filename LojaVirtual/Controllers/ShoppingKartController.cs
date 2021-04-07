using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models.Const;
using LojaVirtual.Libraries.Manager.Shipping;
using LojaVirtual.Libraries.Manager.Frete;
using LojaVirtual.Models;
using LojaVirtual.Controllers.Base;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Filters;

namespace LojaVirtual.Controllers
{
    public class ShoppingKartController : BaseController
    {
        private ClientLogin _clientLogin;
        private IDeliveryAddressRepository _deliveryAddressRepository;

        public ShoppingKartController(ClientLogin clientLogin, IDeliveryAddressRepository deliveryAddressRepository, IProductRepository productRepository, CookieShoppingKart cookieShoppingKart, CookieValorPrazoFrete cookieValorPrazoFrete, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage) : base(productRepository, cookieShoppingKart, cookieValorPrazoFrete, mapper, wsCorreios, calculatePackage)
        {
            _clientLogin = clientLogin;
            _deliveryAddressRepository = deliveryAddressRepository;
        }


        public IActionResult Index()
        {
            List<ProductItem> productKartItemFull = ReadProductDB();

            return View(productKartItemFull);
        }


        public IActionResult AddItem(int Id)
        {
            Product product = _productRepository.Read(Id);

            if (product == null)
            {
                return View("UnavailableItem");
            }
            else
            {
                var Item = new ProductItem() { Id = Id, ItensKartAmount = 1 };
                _cookieShoppingKart.Create(Item);

                return RedirectToAction(nameof(Index));
            }
        }


        [ClientAuthorization]
        public IActionResult DeliveryAddress()
        {
            Client client = _clientLogin.getClient();
            IList<DeliveryAddress> deliveryAddresses = _deliveryAddressRepository.ReadAll(client.Id);

            ViewBag.Client = client;
            ViewBag.Addresses = deliveryAddresses;

            return View();
        }


        public IActionResult ChangeAmount(int Id, int Amount)
        {
            Product product = _productRepository.Read(Id);

            if (Amount < 1)
            {
                return BadRequest(new { message = Message.MSG_E008 });
            }
            else if (Amount > product.Amount)
            {
                return BadRequest(new { message = Message.MSG_E009 });
            }
            else
            {
                var Item = new ProductItem() { Id = Id, ItensKartAmount = Amount };

                _cookieShoppingKart.Update(Item);

                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult DeleteItem(int Id)
        {
            _cookieShoppingKart.Delete(new ProductItem() { Id = Id });

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> CalcularFrete(int cepDestino)
        {
            try
            {
                List<ProductItem> products = ReadProductDB();

                List<Package> packages = _calculatePackage.CalculateProductsPackage(products);

                ValorPrazoFrete valueSEDEX = await _wsCorreios.CalcularFrete(cepDestino.ToString(), CorreiosConst.SEDEX, packages);
                ValorPrazoFrete valueSEDEX10 = await _wsCorreios.CalcularFrete(cepDestino.ToString(), CorreiosConst.SEDEX10, packages);
                ValorPrazoFrete valuePAC = await _wsCorreios.CalcularFrete(cepDestino.ToString(), CorreiosConst.PAC, packages);

                List<ValorPrazoFrete> list = new List<ValorPrazoFrete>();

                if (valueSEDEX != null) list.Add(valueSEDEX);
                if (valueSEDEX10 != null) list.Add(valueSEDEX10);
                if (valuePAC != null) list.Add(valuePAC);

                _cookieValorPrazoFrete.Create(list);

                return Ok(list);
            }
            catch (Exception e)
            {
                _cookieValorPrazoFrete.Delete();

                return BadRequest(e);
            }
        }
    }
}
