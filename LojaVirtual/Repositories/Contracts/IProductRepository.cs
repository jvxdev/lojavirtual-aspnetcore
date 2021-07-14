using LojaVirtual.Models;
using LojaVirtual.Models.ProductAggregator;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IProductRepository
    {
        void Create(Product product);


        Product Read(int id);


        IPagedList<Product> ReadAll(int? page, string search);


        IPagedList<Product> ReadAll(int? page, string search, string ordination, IEnumerable<Category> categories);


        void Update(Product product);


        void ProductsRefundStock(Order order);


        void Delete(int id);


        int TotalProducts();
    }
}
