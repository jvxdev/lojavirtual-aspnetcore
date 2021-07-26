using LojaVirtual.Database;
using LojaVirtual.Libraries.Text;
using LojaVirtual.Models;
using LojaVirtual.Models.Const;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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


        public Order Read(int id)
        {
            return _database.Orders.Include(a => a.OrderSituations).Where(a => a.Id == id).FirstOrDefault();
        }


        public IPagedList<Order> ReadAll(int? page, int clientId)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            return _database.Orders.Include(a => a.OrderSituations).ToPagedList<Order>(pageNumber, registryPerPage);
        }

        public IPagedList<Order> ReadAllOrders(int? page, string codOrder, string cpf)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            var query = _database.Orders.Include(a => a.OrderSituations).Include(a => a.Client).AsQueryable();

            if (cpf != null)
            {
                query = query.Where(a => a.Client.CPF == cpf);
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


        public List<Order> GetAllOrdersBySituation(string status)
        {
            return _database.Orders.Include(a => a.OrderSituations).Include(a => a.Client).Where(a => a.Situation == status).ToList();
        }

        public IPagedList<Order> GetAllClientOrders(int? page, int clientId)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = page ?? 1;

            return _database.Orders.Include(a => a.OrderSituations).Where(a => a.ClientId == clientId).ToPagedList<Order>(pageNumber, registryPerPage);
        }


        public int TotalOrders()
        {
            return _database.Orders.Count();
        }


        public decimal TotalValueOrders()
        {
            return _database.Orders.Sum(a => a.TotalValue);
        }


        public int TotalOrdersCreditCard()
        {
            return _database.Orders.Where(a => a.PaymentForm == PaymentMethodConst.CreditCard).Count();
        }


        public int TotalOrdersBoletoBancario()
        {
            return _database.Orders.Where(a => a.PaymentForm == PaymentMethodConst.Boleto).Count();
        }
    }
}
