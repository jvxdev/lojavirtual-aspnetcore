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
    public class ShoppingKartController : Controller
    {
        private ShoppingKart _shoppingKart;
        private IProductRepository _productRepository;


        public ShoppingKartController(ShoppingKart shoppingKart, IProductRepository productRepository)
        {
            _shoppingKart = shoppingKart;
            _productRepository = productRepository;
        }


        public IActionResult Index()
        {
            return View();
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
                var Item = new ProductItem() { Id = Id, ItensAmount = 1 };
                _shoppingKart.Create(Item);

                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult ChangeAmount(int Id, int Amount)
        {
            var Item = new ProductItem() { Id = Id, ItensAmount = Amount };

            _shoppingKart.Update(Item);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveItem(int Id)
        {
            _shoppingKart.Delete(new ProductItem() { Id = Id });

            return RedirectToAction(nameof(Index));
        }
    }
}
