using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

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


        public void Delete(int Id)
        {
            Image image = _database.Images.Find(Id);
            _database.Remove(Id);
            _database.SaveChanges();
        }


        public void DeleteProductImages(int ProductId)
        {
            IList<Image> images = _database.Images.Where(a => a.ProductId == ProductId).ToList();

            foreach(Image image in images)
            {
                _database.Remove(images);
            }
            _database.SaveChanges();
        }
    }
}
