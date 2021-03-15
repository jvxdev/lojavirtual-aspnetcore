using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IProductRepository
    {
        void Create(Product product);


        Product Read(int Id);


        IPagedList<Product> ReadAll(int? Page, string Search);


        IPagedList<Product> ReadAll(int? Page, string Search, string Ordination, IEnumerable<Category> categories);


        void Update(Product product);


        void Delete(int Id);
    }
}
