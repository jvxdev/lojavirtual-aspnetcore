using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IDeliveryAddressRepository
    {
        void Create(DeliveryAddress deliveryAddress);


        DeliveryAddress Read(int id);


        IList<DeliveryAddress> ReadAll(int clientId);


        void Update(DeliveryAddress deliveryAddress);


        void Delete(int id);
    }
}
