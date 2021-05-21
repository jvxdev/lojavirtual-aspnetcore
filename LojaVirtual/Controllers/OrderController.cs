using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LojaVirtual.Controllers
{
    [ClientAuthorization]
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private ClientLogin _clientLogin;


        public OrderController(IOrderRepository orderRepository, ClientLogin clientLogin)
        {
            _orderRepository = orderRepository;
            _clientLogin = clientLogin;
        }

        public IActionResult Index(int Id)
        {
            Order order = _orderRepository.Read(Id);

            if (order.ClientId != _clientLogin.GetClient().Id)
            {
                return new ContentResult() { Content = "Você não tem permissão para acessar esta página." };
            }

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
