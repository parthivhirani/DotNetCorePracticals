using Microsoft.AspNetCore.Mvc;

namespace Practical17.Controllers
{
    [Route("Error/{action}")]
    public class ErrorHandlerController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
