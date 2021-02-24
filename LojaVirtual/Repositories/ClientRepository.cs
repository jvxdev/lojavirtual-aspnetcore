﻿using LojaVirtual.Database;
using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public class ClientRepository : IClientRepository
    {
        private LojaVirtualContext _database;
        private IConfiguration _conf;


        public ClientRepository(LojaVirtualContext database, IConfiguration configuration)
        {
            _database = database;
            _conf = configuration;
        }

    

        public void Create(Client client)
        {
            _database.Add(client);
            _database.SaveChanges();
        }


        public Client Read(int Id)
        {
            return _database.Clients.Find(Id);
        }


        public IPagedList<Client> ReadAll(int? Page, string Search)
        {
            int registryPerPage = _conf.GetValue<int>("registryPerPage");
            int pageNumber = Page ?? 1;

            var databaseClient = _database.Clients.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                databaseClient = databaseClient.Where(a => a.Name.Contains(Search.Trim()) || a.Email.Contains(Search.Trim()));
            }

            return databaseClient.ToPagedList<Client>(pageNumber, registryPerPage);       
        }


        public void Update(Client client)
        {
            _database.Update(client);
            _database.SaveChanges();
        }


        public void Delete(int Id)
        {
            Client cliente = Read(Id);
            _database.Remove(cliente);
            _database.SaveChanges();
        }


        public Client Login(string Email, string Password)
        {
            Client client = _database.Clients.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
            return client;
        }
    }
}
