using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
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
            return _database.Products.Include(a => a.Images).OrderBy(a => a.Name).Where(a => a.Id == Id).FirstOrDefault();
        }


        public IPagedList<Product> ReadAll(int? Page, string Search)
        {
            return ReadAll(Page, Search, "Oa", null);
        }


        public IPagedList<Product> ReadAll(int? Page, string Search, string Ordination, IEnumerable<Category> categories)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = Page ?? 1;

            var databaseProduct = _database.Products.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                databaseProduct = databaseProduct.Where(a => a.Name.Contains(Search.Trim()));
            }

            if (Ordination == "Oa")
            {
                databaseProduct = databaseProduct.OrderBy(a => a.Name);
            }

            if (Ordination == "Mep")
            {
                databaseProduct = databaseProduct.OrderBy(a => a.Price);
            }

            if (Ordination == "Map")
            {
                databaseProduct = databaseProduct.OrderByDescending(a => a.Price);
            }

            if (categories != null && categories.Count() > 0)
            {
                databaseProduct = databaseProduct.Where(a => categories.Select(b => b.Id).Contains(a.CategoryId));
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
