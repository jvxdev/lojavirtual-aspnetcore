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
            List<ProductItem> productKartItem = _shoppingKart.Read();

            List<ProductItem> productKartItemFull = new List<ProductItem>();

            foreach(var item in productKartItem)
            {
                Product product = _productRepository.Read(item.Id);

                ProductItem productItem = new ProductItem();

                productItem.Id = product.Id;
                productItem.Name = product.Name;
                productItem.Description = product.Description;
                productItem.Images = product.Images;
                productItem.Price = product.Price;
                productItem.ItensKartAmount = item.ItensKartAmount;

                productKartItemFull.Add(productItem);
            }

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
                var Item = new ProductItem() { Id = Id, ItensKartAmount = 1 };
                _shoppingKart.Create(Item);

                return RedirectToAction(nameof(Index));
            }
        }


        public IActionResult ChangeAmount(int Id, int Amount)
        {
            var Item = new ProductItem() { Id = Id, ItensKartAmount = Amount };

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
