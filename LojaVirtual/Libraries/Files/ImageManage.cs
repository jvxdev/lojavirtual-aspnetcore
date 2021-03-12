using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace LojaVirtual.Libraries.Files
{
    public class ImageManage : Controller
    {
        public static string UploadProductImage(IFormFile file)
        {
            var FileName = Path.GetFileName(file.FileName);
            var FileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", FileName);

            using (var stream = new FileStream(FileDirectory, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine("/uploads/temp", FileName).Replace("\\", "/");
        }


        public static bool DeleteProductImage(string path)
        {
            string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path.TrimStart('/'));

            if (System.IO.File.Exists(ImagePath))
            {
                System.IO.File.Delete(ImagePath);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DeleteAllProductImage(List<Image> ImageList)
        {
            int ProductId = 0;

            foreach (var Image in ImageList)
            {
                DeleteProductImage(Image.Path);
                ProductId = Image.ProductId;
            }

            var productDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProductId.ToString());

            if (Directory.Exists(productDirectory))
            {
                Directory.Delete(productDirectory);
            }
        }


        public static List<Image> MoveProductImage(List<string> tempPathList, int ProductId)
        {
            var defProductDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProductId.ToString());

            if (!Directory.Exists(defProductDirectory))
            {
                Directory.CreateDirectory(defProductDirectory);
            }

            List<Image> imagesList = new List<Image>();

            foreach (var tempPath in tempPathList)
            {
                if (!string.IsNullOrEmpty(tempPath))
                {
                    var imageName = Path.GetFileName(tempPath);

                    var defPath = Path.Combine("/uploads", ProductId.ToString(), imageName).Replace("\\", "/");

                    if (defPath != tempPath)
                    {
                        var tempAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", imageName);
                        var defAbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", ProductId.ToString(), imageName);

                        if (System.IO.File.Exists(tempAbsolutePath))
                        {
                            if (System.IO.File.Exists(defAbsolutePath))
                            {
                                System.IO.File.Delete(defAbsolutePath);
                            }

                            System.IO.File.Copy(tempAbsolutePath, defAbsolutePath);

                            if (System.IO.File.Exists(defAbsolutePath))
                            {
                                System.IO.File.Delete(tempAbsolutePath);
                            }

                            imagesList.Add(new Image() { Path = Path.Combine("/uploads", ProductId.ToString(), imageName).Replace("\\", "/"), ProductId = ProductId });
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        imagesList.Add(new Image() { Path = Path.Combine("/uploads", ProductId.ToString(), imageName).Replace("\\", "/"), ProductId = ProductId });
                    }
                }
            }

            return imagesList;
        }
    }
}
