using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ColaboratorRepository : IColaboratorRepository
    {
        private LojaVirtualContext _database;

        public ColaboratorRepository(LojaVirtualContext database)
        {
            _database = database;
        }


        public void Create(Colaborator colaborator)
        {
            _database.Add(colaborator);
            _database.SaveChanges();
        }


        public Colaborator Read(int Id)
        {
            return _database.Colaborators.Find(Id);
        }


        public IEnumerable<Colaborator> ReadAll()
        {
            return _database.Colaborators.ToList();
        }


        public void Update(Colaborator colaborator)
        {
            _database.Update(colaborator);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Colaborator colaborator = Read(Id);
            _database.Remove(colaborator);
            _database.SaveChanges();
        }


        public Colaborator Login(string Email, string Password)
        {
            Colaborator colaborator = _database.Colaborators.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            return colaborator;
        }
    }
}
