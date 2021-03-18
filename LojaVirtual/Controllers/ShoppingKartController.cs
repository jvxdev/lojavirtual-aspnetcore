using LojaVirtual.Libraries.ShoppingKart;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace LojaVirtual.Controllers
{
    public class ShoppingKartController : Controller
    {
        private ShoppingKart _shoppingKart;
        private IProductRepository _productRepository;
        private IMapper _mapper;

        public ShoppingKartController(ShoppingKart shoppingKart, IProductRepository productRepository, IMapper mapper)
        {
            _shoppingKart = shoppingKart;
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            List<ProductItem> productKartItem = _shoppingKart.Read();

            List<ProductItem> productKartItemFull = new List<ProductItem>();

            foreach(var item in productKartItem)
            {
                Product product = _productRepository.Read(item.Id);

                ProductItem productItem = _mapper.Map<ProductItem>(product);
                productItem.ItensKartAmount = item.ItensKartAmount;

                productKartItemFull.Add(productItem);
            }

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
            var Item = new ProductItem() { Id = Id, ItensKartAmount = Amount };

            _shoppingKart.Update(Item);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteItem(int Id)
        {
            _shoppingKart.Delete(new ProductItem() { Id = Id });

            return RedirectToAction(nameof(Index));
        }
    }
}
