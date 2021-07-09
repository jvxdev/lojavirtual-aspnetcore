using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace LojaVirtual.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error500()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.Message = exception.Error.Message;

            return View();
        }


        [Route("Error/{statusCode}")]
        public IActionResult GenericError(int statusCode)
        {
            ViewBag.Message = string.Format("Código de erro: {0}. Status: {1}", statusCode, ReasonPhrases.GetReasonPhrase(statusCode));

            return View("Error500");
        }
    }
}
