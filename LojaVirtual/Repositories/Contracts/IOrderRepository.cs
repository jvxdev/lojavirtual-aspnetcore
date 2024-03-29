﻿using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IOrderRepository
    {
        void Create(Order order);


        Order Read(int id);


        IPagedList<Order> ReadAll(int? page, int clientId);


        IPagedList<Order> ReadAllOrders(int? page, string codOrder, string cpf);


        List<Order> GetAllOrdersBySituation(string status);


        IPagedList<Order> GetAllClientOrders(int? page, int clientId);


        void Update(Order order);


        int TotalOrders();


        decimal TotalValueOrders();


        int TotalOrdersCreditCard();


        int TotalOrdersBoletoBancario();
    }
}
