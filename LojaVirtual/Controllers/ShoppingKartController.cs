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
using LojaVirtual.Models;

namespace LojaVirtual.Controllers
{
    public class ShoppingKartController : Controller
    {
        private ShoppingKart _shoppingKart;
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private WSCorreiosCalcularFrete _wsCorreios;
        private CalculatePackage _calculatePackage;


        public ShoppingKartController(ShoppingKart shoppingKart, IProductRepository productRepository, IMapper mapper, WSCorreiosCalcularFrete wsCorreios, CalculatePackage calculatePackage)
        {
            _shoppingKart = shoppingKart;
            _productRepository = productRepository;
            _mapper = mapper;
            _wsCorreios = wsCorreios;
            _calculatePackage = calculatePackage;
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
                _shoppingKart.Create(Item);

                return RedirectToAction(nameof(Index));
            }
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

                _shoppingKart.Update(Item);

                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult DeleteItem(int Id)
        {
            _shoppingKart.Delete(new ProductItem() { Id = Id });

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

                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        private List<ProductItem> ReadProductDB()
        {
            List<ProductItem> productKartItem = _shoppingKart.Read();

            List<ProductItem> productKartItemFull = new List<ProductItem>();

            foreach (var item in productKartItem)
            {
                Product product = _productRepository.Read(item.Id);

                ProductItem productItem = _mapper.Map<ProductItem>(product);
                productItem.ItensKartAmount = item.ItensKartAmount;

                productKartItemFull.Add(productItem);
            }

            return productKartItemFull;
        }
    }
}
