using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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