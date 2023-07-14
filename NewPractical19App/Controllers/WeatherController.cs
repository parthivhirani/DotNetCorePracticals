using Microsoft.AspNetCore.Mvc;
using NewPractical19;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NewPractical19App.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Request.Cookies["User"].ToString());
            var weatherResult = await _httpClient.GetAsync("https://localhost:7289/WeatherForecast");
            if (weatherResult != null && weatherResult.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<List<WeatherForecast>>(weatherResult.Content.ReadAsStringAsync().Result);
                return View(content);
            }
            else
            {
                return RedirectToAction("AccessError");
            }
        }

        public async Task<IActionResult> AccessError()
        {
            return View();
        }
    }
}