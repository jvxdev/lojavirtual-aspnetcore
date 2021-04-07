using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IDeliveryAddressRepository
    {
        void Create(DeliveryAddress deliveryAddress);


        DeliveryAddress Read(int Id);


        IList<DeliveryAddress> ReadAll(int ClientId);


        void Update(DeliveryAddress deliveryAddress);


        void Delete(int Id);
    }
}
