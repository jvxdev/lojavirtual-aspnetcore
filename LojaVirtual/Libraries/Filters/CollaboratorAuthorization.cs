using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filters
{
    public class CollaboratorAuthorization : Attribute, IAuthorizationFilter
    {
        private string _authorizedCollaborator;

        public CollaboratorAuthorization(string AuthorizedCollaborator = "C")
        {
            _authorizedCollaborator = AuthorizedCollaborator;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            CollaboratorLogin _colaboratorLogin;

            _colaboratorLogin = (CollaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(CollaboratorLogin));

            Collaborator collaborator = _colaboratorLogin.getCollaborator();

            if (collaborator == null)
            
            {
                 context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if(collaborator.Position == "C" && _authorizedCollaborator == "G")
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
