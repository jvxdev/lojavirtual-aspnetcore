using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private LojaVirtualContext _database;
        private IConfiguration _conf;


        public ProductRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }


        public void Create(Product product)
        {
            _database.Add(product);
            _database.SaveChanges();
        }


        public Product Read(int Id)
        {
            return _database.Products.Include(a => a.Images).Where(a => a.Id == Id).FirstOrDefault();
        }


        public IPagedList<Product> ReadAll(int? Page, string Search)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = Page ?? 1;

            var databaseProduct = _database.Products.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                databaseProduct = databaseProduct.Where(a => a.Name.Contains(Search.Trim()));
            }

            return databaseProduct.Include(a => a.Images).ToPagedList<Product>(pageNumber, registryPerPage);
        }


        public void Update(Product product)
        {
            _database.Update(product);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Product product = Read(Id);
            _database.Remove(product);
            _database.SaveChanges();
        }
    }
}
