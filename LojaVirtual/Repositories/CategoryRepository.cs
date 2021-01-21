using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        LojaVirtualContext _database;

        public CategoryRepository(LojaVirtualContext database)
        {
            _database = database;
        }


        public void Create(Category category)
        {
            _database.Add(category);
            _database.SaveChanges();
        }


        public Category Read(int Id)
        {
            return _database.Categories.Find(Id);
        }


        public IEnumerable<Category> ReadAll()
        {
            return _database.Categories.ToList();
        }


        public void Update(Category category)
        {
            _database.Update(category);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Category category = Read(Id);
            _database.Remove(category);
            _database.SaveChanges();
        }
    }
}
