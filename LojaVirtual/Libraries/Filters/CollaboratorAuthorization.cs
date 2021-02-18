using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filters
{
    public class CollaboratorAuthorization : Attribute, IAuthorizationFilter
    {
        private string _authorizedCollaborator;


        public CollaboratorAuthorization(string AuthorizedCollaborator = CollaboratorPositionConst.Commun)
        {
            _authorizedCollaborator = AuthorizedCollaborator;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            CollaboratorLogin _colaboratorLogin;

            _colaboratorLogin = (CollaboratorLogin)context.HttpContext.RequestServices.GetService(typeof(CollaboratorLogin));

            Collaborator collaborator = _colaboratorLogin.GetCollaborator();

            if (collaborator == null)
            
            {
                 context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if(collaborator.Position == CollaboratorPositionConst.Commun && _authorizedCollaborator == CollaboratorPositionConst.Manager)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
