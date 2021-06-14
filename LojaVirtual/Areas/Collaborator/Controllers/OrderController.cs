using LojaVirtual.Libraries.Filters;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;


        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public IActionResult Index(int? Page, string codOrder, string cpf)
        {
            var orders = _orderRepository.ReadAllOrders(Page, codOrder, cpf);

            return View(orders);
        }


        public IActionResult Show(int Id)
        {
            Order order = _orderRepository.Read(Id);

            return View(order);
        }
    }
}
