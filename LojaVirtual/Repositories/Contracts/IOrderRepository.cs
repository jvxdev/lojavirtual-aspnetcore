using LojaVirtual.Models;
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
