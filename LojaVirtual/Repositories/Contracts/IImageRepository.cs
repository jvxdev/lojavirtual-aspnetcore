using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IImageRepository
    {
        void Create(Image image);


        void ImagesUpload(List<Image> imagesList, int productId);


        void Delete(int id);


        void DeleteAllProductImages(int productId);
    }
}
