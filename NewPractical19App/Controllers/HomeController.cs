using Microsoft.AspNetCore.Mvc;
using NewPractical19App.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using NewPractical19.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using NuGet.Protocol;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NewPractical19;

namespace NewPractical19App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["User"] != null)
            {
                ViewBag.UNmae = "User";
            }
            else
            {
                ViewBag.UNmae = "Guest";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _httpClient.PostAsJsonAsync("https://localhost:7289/api/Authenticate/login", model);
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TokenModel>(data);

                if (result.StatusCode == (HttpStatusCode)200 && response != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token.ToString());
                    Response.Cookies.Append("User", response.Token);
                    return RedirectToAction("Index");
                }
                ViewBag.Error = "Invalid user!";
                return View(model);
            }
            else
            {
                ViewBag.Error = "Invalid model state";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _httpClient.PostAsJsonAsync("https://localhost:7289/api/Authenticate/register", model);
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TokenModel>(data);

                if (result.StatusCode == (HttpStatusCode)200 && response != null)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.Error = "Please fill all the details";
                return View(model);
            }
            else
            {
                ViewBag.Error = "Invalid model state";
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("User");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}