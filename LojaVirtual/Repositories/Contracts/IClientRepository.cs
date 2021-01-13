using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClientRepository
    {
        void Create(Client client);

        Client Read(int Id);

        IEnumerable<Client> ReadAll();

        void Update(Client client);

        void Delete(int Id);

        Client Login(string Email, string Password);
    }
}