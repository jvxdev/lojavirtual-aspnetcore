using LojaVirtual.Libraries.Lang;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filters
{
    public class PaymentCookieAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var _cookie = (Cookie.Cookie)context.HttpContext.RequestServices.GetService(typeof(Cookie.Cookie));

            var shoppingKart = _cookie.Read("ShoppingKart.Buy", true);
            var tipoFreteSelected = _cookie.Read("ShoppingKart.tipoFrete", false);
            var valueFrete = _cookie.Read("ShoppingKart.FreteValue", true);

            if (shoppingKart == null)
            {
                ((Controller)context.Controller).TempData["MSG_E"] = Message.MSG_E012;
                context.Result = new RedirectToActionResult("Index", "ShoppingKart", null);
            }

            if (tipoFreteSelected == null || valueFrete == null)
            {
                ((Controller)context.Controller).TempData["MSG_E"] = Message.MSG_E010;
                context.Result = new RedirectToActionResult("DeliveryAddress", "ShoppingKart", null);
            }
        }
    }
}
