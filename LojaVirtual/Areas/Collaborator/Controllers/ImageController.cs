using LojaVirtual.Libraries.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Collaborator.Controllers
{
    [Area("Collaborator")]
    public class ImageController : Controller
    {
        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
           var Path = ImageManage.UploadProductImage(file);

            if (Path.Length > 0)
            {
                return Ok(new { path = Path });
            }
            else
            {
                return new StatusCodeResult(500);
            }

            return View();
        }


        public IActionResult Delete(string path)
        {
            if (ImageManage.DeleteProductImage(path))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
