using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorePractical16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
                _logger = logger;
        }


        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Calling Get() method of HelloWorld api");
            return "Hello World";
        }
    }
}
