using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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
