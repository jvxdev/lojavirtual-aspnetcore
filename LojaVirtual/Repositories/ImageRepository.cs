using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private LojaVirtualContext _database;


        public ImageRepository(LojaVirtualContext database)
        {
            _database = database;
        }


        public void Create(Image image)
        {
            _database.Add(image);
            _database.SaveChanges();
        }


        public void ImagesUpload(List<Image> imagesList, int ProductId)
        {
            if (imagesList != null && imagesList.Count > 0)
            {
                foreach (var image in imagesList)
                {
                    Create(image);
                }
            }
        }


        public void Delete(int Id)
        {
            Image image = _database.Images.Find(Id);
            _database.Remove(image);
            _database.SaveChanges();
        }


        public void DeleteAllProductImages(int ProductId)
        {
            List<Image> images = _database.Images.Where(a => a.ProductId == ProductId).ToList();

            foreach (Image image in images)
            {
                _database.Remove(image);
            }

            _database.SaveChanges();
        }
    }
}
