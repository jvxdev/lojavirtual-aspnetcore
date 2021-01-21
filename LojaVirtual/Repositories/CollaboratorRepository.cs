using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private LojaVirtualContext _database;

        public CollaboratorRepository(LojaVirtualContext database)
        {
            _database = database;
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


        public IEnumerable<Collaborator> ReadAll()
        {
            return _database.Collaborators.ToList();
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
