using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        void Create(Category category);


        Category Read(int Id);


        IEnumerable<Category> ReadAll();


        void Update(Category category);


        void Delete(int Id);
    }
}
