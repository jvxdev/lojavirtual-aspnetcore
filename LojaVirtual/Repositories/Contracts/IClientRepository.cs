using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClientRepository
    {
        void Create(Client client);


        Client Read(int Id);


        IPagedList<Client> ReadAll(int? Page, string Search);


        void Update(Client client);


        void Delete(int Id);


        Client Login(string Email, string Password);
    }
}