using LojaVirtual.Models;
using System.Collections.Generic;

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
