using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LojaVirtual.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;


        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index(int Id)
        {
            Order order = _orderRepository.Read(Id);

            ViewBag.Products = JsonConvert.DeserializeObject<List<ProductItem>>(order.ProductsData,
            new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>() }    
            );

            var transaction = JsonConvert.DeserializeObject<TransactionPagarMe>(order.TransactionData);
            var fee = transaction.Shipping.Fee;

            ViewBag.Transaction = transaction;

            return View(order);
        }
    }
}
