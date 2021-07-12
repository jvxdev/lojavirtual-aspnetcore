using LojaVirtual.Database;
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public class ClientRepository : IClientRepository
    {
        private LojaVirtualContext _database;
        private IConfiguration _conf;


        public ClientRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }



        public void Create(Client client)
        {
            _database.Add(client);
            _database.SaveChanges();
        }


        public Client Read(int id)
        {
            return _database.Clients.Find(id);
        }


        public IPagedList<Client> ReadAll(int? page, string search)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            var databaseClient = _database.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                databaseClient = databaseClient.Where(a => a.Name.Contains(search.Trim()) || a.Email.Contains(search.Trim()));
            }

            return databaseClient.ToPagedList<Client>(pageNumber, registryPerPage);
        }


        public void Update(Client client)
        {
            _database.Update(client);
            _database.SaveChanges();
        }


        public void Delete(int id)
        {
            Client cliente = Read(id);
            _database.Remove(cliente);
            _database.SaveChanges();
        }


        public Client Login(string email, string password)
        {
            Client client = _database.Clients.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return client;
        }
    }
}
