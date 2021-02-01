using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        void Create(Category category);


        Category Read(int Id);


        IPagedList<Category> ReadAll(int? page);


        void Update(Category category);


        void Delete(int Id);
    }
}
