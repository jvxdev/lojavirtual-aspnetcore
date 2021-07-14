using LojaVirtual.Database;
using LojaVirtual.Libraries.Json.Resolver;
using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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


        public Product Read(int id)
        {
            return _database.Products.Include(a => a.Images).OrderBy(a => a.Name).Where(a => a.Id == id).FirstOrDefault();
        }


        public IPagedList<Product> ReadAll(int? Page, string Search)
        {
            return ReadAll(Page, Search, "Oa", null);
        }


        public IPagedList<Product> ReadAll(int? page, string search, string ordination, IEnumerable<Category> categories)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            var databaseProduct = _database.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                databaseProduct = databaseProduct.Where(a => a.Name.Contains(search.Trim()));
            }

            if (ordination == "Oa")
            {
                databaseProduct = databaseProduct.OrderBy(a => a.Name);
            }

            if (ordination == "Mep")
            {
                databaseProduct = databaseProduct.OrderBy(a => a.Price);
            }

            if (ordination == "Map")
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


        public void ProductsRefundStock(Order order)
        {
            List<ProductItem> products = JsonConvert.DeserializeObject<List<ProductItem>>(order.ProductsData, new JsonSerializerSettings() { ContractResolver = new ProductItemResolver<List<ProductItem>>() });

            foreach (var product in products)
            {
                Product productDB = Read(product.Id);

                productDB.Stock += product.ChosenUnits;

                Update(productDB);
            }
        }


        public void Delete(int id)
        {
            Product product = Read(id);
            _database.Remove(product);
            _database.SaveChanges();
        }


        public int TotalProducts()
        {
            return _database.Products.Count();
        }
    }
}
