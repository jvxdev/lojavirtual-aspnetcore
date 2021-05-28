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
    public class OrderController : Controller
    {
        private ClientLogin _clientLogin;
        private IOrderRepository _orderRepository;


        public OrderController(ClientLogin clientLogin, IOrderRepository orderRepository)
        {
            _clientLogin = clientLogin;
            _orderRepository = orderRepository;
        }


        public IActionResult Index(int? Page)
        {
            Models.Client client = _clientLogin.GetClient();

            var orders = _orderRepository.ReadAll(Page, client.Id);

            return View(orders);
        }


        public IActionResult Show(int Id)
        {
            Models.Client client = _clientLogin.GetClient();

            Order order = _orderRepository.Read(Id);

            if (order.ClientId != client.Id)
            {
                return new ContentResult() { Content = "Acesso negado." };
            }

            return View(order);
        }
    }
}