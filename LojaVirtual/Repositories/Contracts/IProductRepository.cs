using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IProductRepository
    {
        void Create(Product product);


        Product Read(int Id);


        IPagedList<Product> ReadAll(int? Page, string Search);


        void Update(Product product);


        void Delete(int Id);
    }
}
