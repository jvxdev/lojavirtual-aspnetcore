using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        const int registryPerPage = 10;
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
            return _database.Categories;
        }


        public IPagedList<Category> ReadAll(int? page)
        {
            int pageNumber = page ?? 1;
            return _database.Categories.Include(a=>a.FatherCategory).ToPagedList<Category>(pageNumber, registryPerPage);
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
