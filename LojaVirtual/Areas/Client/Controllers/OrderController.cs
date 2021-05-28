using LojaVirtual.Libraries.Login;
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
    }
}
