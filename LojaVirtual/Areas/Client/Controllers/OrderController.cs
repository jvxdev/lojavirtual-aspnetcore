using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Client.Controllers
{
    [Area("Client")]
    [ClientAuthorization]
    public class OrderController : Controller
    {
        private ClientLogin _clientLogin;
        private IOrderRepository _orderRepository;


        public OrderController(ClientLogin clientLogin, IOrderRepository orderRepository)
        {
            _clientLogin = clientLogin;
            _orderRepository = orderRepository;
        }


        public IActionResult Index(int? page)
        {
            Models.Client client = _clientLogin.GetClient();

            var orders = _orderRepository.ReadAll(page, client.Id);

            return View(orders);
        }


        [HttpGet]
        public IActionResult Show(int id)
        {
            Models.Client client = _clientLogin.GetClient();

            Order order = _orderRepository.Read(id);

            if (order.ClientId != client.Id)
            {
                return new StatusCodeResult(403);
            }

            return View(order);
        }
    }
}