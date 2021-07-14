using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClientRepository
    {
        void Create(Client client);


        Client Read(int id);


        IPagedList<Client> ReadAll(int? page, string search);


        void Update(Client client);


        void Delete(int id);


        Client Login(string email, string password);


        int TotalClients();
    }
}