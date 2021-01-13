using LojaVirtual.Database;
using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public class ClientRepository : IClientRepository
    {
        private LojaVirtualContext _database;

        public ClientRepository(LojaVirtualContext database)
        {
            _database = database;
        }

    

        public void Create(Client client)
        {
            _database.Add(client);
            _database.SaveChanges();
        }


        public Client Read(int Id)
        {
            return _database.Clients.Find(Id);
        }


        public IEnumerable<Client> ReadAll()
        {
            return _database.Clients.ToList();
        }


        public void Update(Client client)
        {
            _database.Update(client);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Client cliente = Read(Id);
            _database.Remove(cliente);
            _database.SaveChanges();
        }


        public Client Login(string Email, string Password)
        {
            Client client = _database.Clients.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            return client;
        }
    }
}
