using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class OrderSituationViewComponent : ViewComponent
    {
        public OrderSituationViewComponent()
        {
        }


        public async Task<IViewComponentResult> InvokeAsync(Order order)
        {
            return View();
        }
    }
}
