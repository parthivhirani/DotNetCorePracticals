using Microsoft.AspNetCore.Mvc;
using Practical20.Models;
using System.Diagnostics;

namespace Practical20.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ambiguous()
        {
            return StatusCode(300);
        }
        public IActionResult BadRequest()
        {
            return StatusCode(400);
        }
        public IActionResult InternalServerError()
        {
            return StatusCode(500);
        }
        public IActionResult LoopDetected()
        {
            return StatusCode(508);
        }
        public IActionResult NotFound()
        {
            return StatusCode(404);
        }
        public IActionResult UnAuthorizeError()
        {
            return StatusCode(401);
        }
    }
}