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
    public class OrderSituationRepository : IOrderSituationRepository
    {
        private LojaVirtualContext _database;
        private IConfiguration _conf;


        public OrderSituationRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }


        public void Create(OrderSituation order)
        {
            _database.Add(order);
            _database.SaveChanges();
        }


        public OrderSituation Read(int Id)
        {
            return _database.OrderSituations.Include(a => a.Order).Where(a => a.Id == Id).FirstOrDefault();
        }


        public void Update(OrderSituation order)
        {
            _database.Update(order);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            OrderSituation order = Read(Id);
            _database.Remove(order);
            _database.SaveChanges();
        }
    }
}
