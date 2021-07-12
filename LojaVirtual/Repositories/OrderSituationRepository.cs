using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

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


        public OrderSituation Read(int id)
        {
            return _database.OrderSituations.Include(a => a.Order).Where(a => a.Id == id).FirstOrDefault();
        }


        public void Update(OrderSituation order)
        {
            _database.Update(order);
            _database.SaveChanges();
        }


        public void Delete(int id)
        {
            OrderSituation order = Read(id);
            _database.Remove(order);
            _database.SaveChanges();
        }
    }
}
