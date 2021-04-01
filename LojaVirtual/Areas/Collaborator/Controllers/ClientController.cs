using LojaVirtual.Libraries.Filters;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index(int? Page, string Search)
        {
            IPagedList<Models.Client> clients = _clientRepository.ReadAll(Page, Search);
            return View(clients);
        }


        [HttpReferer]
        public IActionResult ActivateDeactivate(int Id)
        {
            Models.Client client = _clientRepository.Read(Id);

            if (client.Situation == SituationConst.Active)
            {
                client.Situation = SituationConst.Deactivate;
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
