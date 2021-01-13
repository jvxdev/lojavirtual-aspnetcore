using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filters
{
    public class ColaboratorAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            ColaboratorLogin _colaboratorLogin;

            _colaboratorLogin = (ColaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(ColaboratorLogin));

            Colaborator colaborator = _colaboratorLogin.getColaborator();

            if (colaborator == null)
            
            {
                 context.Result = new ContentResult() { Content = "Você não tem permissão para acessar esta página." };
            }
        }
    }
}
