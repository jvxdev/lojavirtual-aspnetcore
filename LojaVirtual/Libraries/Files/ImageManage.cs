using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
