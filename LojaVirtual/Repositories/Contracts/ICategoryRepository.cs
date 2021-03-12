using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        void Create(Category category);


        Category Read(int Id);


        IEnumerable<Category> ReadAll();


        IPagedList<Category> ReadAll(int? page);


        void Update(Category category);


        void Delete(int Id);
    }
}
