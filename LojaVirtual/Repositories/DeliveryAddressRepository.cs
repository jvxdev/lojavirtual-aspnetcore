using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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


        public DeliveryAddress Read(int Id)
        {
            return _database.DeliveryAddresses.Find(Id);
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


        public void Delete(int Id)
        {
            DeliveryAddress deliveryAddress = Read(Id);
            _database.Remove(deliveryAddress);
            _database.SaveChanges();
        }
    }
}
