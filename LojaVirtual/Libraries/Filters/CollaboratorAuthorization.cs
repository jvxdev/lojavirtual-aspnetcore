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
    public class CollaboratorAuthorization : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            CollaboratorLogin _colaboratorLogin;

            _colaboratorLogin = (CollaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(CollaboratorLogin));

            Collaborator colaborator = _colaboratorLogin.getCollaborator();

            if (colaborator == null)
            
            {
                 context.Result = new RedirectToActionResult("Login", "Home", null);
            }
        }
    }
}
