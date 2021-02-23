using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    [CollaboratorAuthorization]
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;


        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public IActionResult Index(int? page)
        {
            IPagedList<Client> clients = _clientRepository.ReadAll(page);
            return View(clients);
        }


        [HttpReferer]
        public IActionResult ActivateDeactivate(int Id)
        {
            Client client = _clientRepository.Read(Id);

            if (client.Situation == SituationConst.Active)
            {
                client.Situation = SituationConst.Disabled;
            }
            else
            {
                client.Situation = SituationConst.Active;
            }

            _clientRepository.Update(client);

            TempData["MSG_S"] = Message.MSG_S003;

            return RedirectToAction(nameof(Index));
        }
    }
}
