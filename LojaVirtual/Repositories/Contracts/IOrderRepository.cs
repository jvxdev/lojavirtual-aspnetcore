using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IOrderRepository
    {
        void Create(Order order);


        Order Read(int Id);


        IPagedList<Order> ReadAll(int? Page, int ClientId);


        IPagedList<Order> ReadAllOrders(int? Page, string codOrder, string cpf);


        void Update(Order order);
    }
}
