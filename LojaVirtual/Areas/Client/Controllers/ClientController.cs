using LojaVirtual.Libraries.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Client.Controllers
{
    public class ClientController : Controller
    {
        [ClientAuthorization]
        public IActionResult Index()
        {
            return View();
        }


        [ClientAuthorization]
        public IActionResult Update()
        {
            return View();
        }
    }
}
