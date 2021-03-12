using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filters
{
    public class HttpRefererAttribute : Attribute
    {
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
