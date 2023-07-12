using Microsoft.AspNetCore.Mvc;

namespace Practical20.Controllers
{
    public class ErrorHandlerController : Controller
    {

        public IActionResult Ambiguous()
        {
            return View();
        }
        public IActionResult BadRequest()
        {
            return View();
        }
        public IActionResult InternalServerError()
        {
            return View();
        }
        public IActionResult LoopDetected()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
