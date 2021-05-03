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
    public class OrderRepository : IOrderRepository
    {
        private LojaVirtualContext _database;
        private IConfiguration _conf;


        public OrderRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }


        public void Create(Order order)
        {
            _database.Add(order);
            _database.SaveChanges();
        }


        public Order Read(int Id)
        {
            return _database.Orders.Include(a => a.OrderSituations).Where(a => a.Id == Id).FirstOrDefault();
        }


        public IPagedList<Order> ReadAll(int? Page, int ClientId)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = Page ?? 1;

            return _database.Orders.Include(a => a.OrderSituations).ToPagedList<Order>(pageNumber, registryPerPage);
        }


        public void Update(Order product)
        {
            _database.Update(product);
            _database.SaveChanges();
        }
    }
}
