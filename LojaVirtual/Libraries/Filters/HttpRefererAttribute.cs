using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filters
{
    public class HttpRefererAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string referer = context.HttpContext.Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(referer))
            {
                context.Result = new ContentResult() { Content = "Você não tem permissão para acessar esta página." };
            }
            else
            {
                Uri uri = new Uri(referer);

                string refererHost = uri.Host;
                string serverHost = context.HttpContext.Request.Host.Host;

                if (refererHost != serverHost)
                {
                    context.Result = new ContentResult() { Content = "Você não tem permissão para acessar esta página." };
                }
            }
        }
    }
}
