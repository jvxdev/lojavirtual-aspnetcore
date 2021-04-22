using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filters
{
    public class ClientAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ClientLogin _clientLogin;

            _clientLogin = (ClientLogin)context.HttpContext.RequestServices.GetService(typeof(ClientLogin));

            Client client = _clientLogin.getClient();

            if (client == null)

            {
                context.Result = new ContentResult() { Content = "Você não tem permissão para acessar esta página." };
            }
        }
    }
}
