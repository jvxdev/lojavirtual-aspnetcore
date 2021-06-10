using LojaVirtual.Database;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
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

        public IPagedList<Order> ReadAllOrders(int? Page, string codOrder, string Cpf)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = Page ?? 1;

            var query = _database.Orders.Include(a => a.OrderSituations).Include(a => a.Client).AsQueryable();

            if (Cpf != null)
            {
                query.Where(a => a.Client.CPF == Cpf);
            }

            if (codOrder != null)
            {
                string transactionId = string.Empty;

                int id = Mask.ExtractCodOrder(codOrder, out transactionId);

                query = query.Where(a => a.Id == id && a.TransactionId == transactionId);
            }

            return query.ToPagedList<Order>(pageNumber, registryPerPage);
        }

        public void Update(Order order)
        {
            _database.Update(order);
            _database.SaveChanges();
        }
    }
}
