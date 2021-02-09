using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private IConfiguration _conf;
        private LojaVirtualContext _database;

        public CollaboratorRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }


        public void Create(Collaborator collaborator)
        {
            _database.Add(collaborator);
            _database.SaveChanges();
        }


        public Collaborator Read(int Id)
        {
            return _database.Collaborators.Find(Id);
        }


        public IPagedList<Collaborator> ReadAll(int? page)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            return _database.Collaborators.Where(a => a.Position != "G").ToPagedList<Collaborator>(pageNumber, registryPerPage);
        }


        public void Update(Collaborator collaborator)
        {
            _database.Update(collaborator);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Collaborator collaborator = Read(Id);
            _database.Remove(collaborator);
            _database.SaveChanges();
        }


        public Collaborator Login(string Email, string Password)
        {
            Collaborator colaborator = _database.Collaborators.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            return colaborator;
        }
    }
}
