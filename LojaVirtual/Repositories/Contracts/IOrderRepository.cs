using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IOrderRepository
    {
        void Create(Order order);


        Order Read(int Id);


        IPagedList<Order> ReadAll(int? Page, int ClientId);


        void Update(Order order);
    }
}
