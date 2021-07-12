using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class DeliveryAddressRepository : IDeliveryAddressRepository
    {
        private LojaVirtualContext _database;


        public DeliveryAddressRepository(LojaVirtualContext database)
        {
            _database = database;
        }


        public void Create(DeliveryAddress deliveryAddress)
        {
            _database.Add(deliveryAddress);
            _database.SaveChanges();
        }


        public DeliveryAddress Read(int id)
        {
            return _database.DeliveryAddresses.Find(id);
        }


        public IList<DeliveryAddress> ReadAll(int clientId)
        {
            return _database.DeliveryAddresses.Where(a => a.ClientId == clientId).ToList();
        }


        public void Update(DeliveryAddress deliveryAddress)
        {
            _database.Update(deliveryAddress);
            _database.SaveChanges();
        }


        public void Delete(int id)
        {
            DeliveryAddress deliveryAddress = Read(id);
            _database.Remove(deliveryAddress);
            _database.SaveChanges();
        }
    }
}
