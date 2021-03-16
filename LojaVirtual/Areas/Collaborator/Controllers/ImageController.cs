using LojaVirtual.Libraries.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
