using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IOrderSituationRepository
    {
        void Create(OrderSituation order);


        OrderSituation Read(int Id);


        void Update(OrderSituation order);


        void Delete(int Id);
    }
}
