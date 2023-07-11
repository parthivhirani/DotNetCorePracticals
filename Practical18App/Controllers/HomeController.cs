using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Practical18App.Models;
using Practical18App.ViewModel;
using System.Diagnostics;
using System.Net;

namespace Practical18App.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7128/api/Student");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                List<StudentViewModel> students = JsonConvert.DeserializeObject<List<StudentViewModel>>(data);

                return View(students);
            }

            return View();

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            model.Gender = (Gender)Convert.ToInt32(model.Gender);
            var result = await _httpClient.PostAsJsonAsync("https://localhost:7128/api/Student", model);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                return RedirectToAction("Index", "Home");

            }

            ModelState.AddModelError("", "Data not added!");
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            string url = $"https://localhost:7128/api/Student/{id}";

            var result = await _httpClient.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {
                StudentViewModel student = ConvertToStudentViewModelFromResponse(await result.Content.ReadAsStringAsync());
                return View(student);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel model)
        {
            string url = "https://localhost:7128/api/Student/" + model.Id.ToString();

            var result = await _httpClient.PutAsJsonAsync(url, model);

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                model.Gender = (Gender)Convert.ToInt32(model.Gender);
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            string url = "https://localhost:7128/api/Student/" + id.ToString();

            var result = await _httpClient.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                var student = ConvertToStudentViewModelFromResponse(await result.Content.ReadAsStringAsync());
                return View(student);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentViewModel model)
        {
            string url = "https://localhost:7128/api/Student/" + model.Id.ToString();

            var result = await _httpClient.DeleteAsync(url);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                ModelState.AddModelError("", "Model is not found or already deleted!");
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            string url = "https://localhost:7128/api/Student/" + id.ToString();

            var result = await _httpClient.GetAsync(url);

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }

            if (result.StatusCode == HttpStatusCode.OK)
            {


                StudentViewModel student = ConvertToStudentViewModelFromResponse(await result.Content.ReadAsStringAsync());
                return View(student);
            }

            return NotFound();
        }


        private Student ToStudent(StudentViewModel model)
        {
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<StudentViewModel, Student>());

            var mapper = mapperConfiguration.CreateMapper();

            return mapper.Map<Student>(model);
        }

        private StudentViewModel ConvertToStudentViewModelFromResponse(string data)
        {
            return JsonConvert.DeserializeObject<StudentViewModel>(data);
        }

    }
}