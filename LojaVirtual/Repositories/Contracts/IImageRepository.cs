using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IImageRepository
    {
        void Create(Image image);


        void Delete(int Id);


        void DeleteProductImages(int ProductId);
    }
}
