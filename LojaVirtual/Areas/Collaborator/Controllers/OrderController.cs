using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    public class OrderController : Controller
    {
        [Area("Collaborator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
