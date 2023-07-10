using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practical17.Models;
using Practical17.Repository;

namespace Practical17.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;


        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            return _repository.GetStudents() != null ? View(_repository.GetStudents()) : Problem("There is no Students!");
        }

        public IActionResult Details(int? id)
        {
            if (id == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(id ?? 0);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(id ?? 0);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.RollNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateStudentAsync(id, student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(id ?? 0);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_repository.GetStudents() == null)
            {
                return Problem("Entity set 'DatabaseContext.Students'  is null.");
            }
            var student = _repository.GetStudentById(id);
            if (student != null)
            {
                await _repository.DeleteStudentAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
