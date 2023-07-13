using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Practical17.Models;
using Practical17.Repository;
using Practical17.Security;

namespace Practical17.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _repository;
        private readonly IDataProtector protector;


        public StudentController(IStudentRepository repository, 
                IDataProtectionProvider dataProtectionProvider,
                DataProtectionPurposeString dataProtectionPurposeString)
        {
            _repository = repository;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeString.EmployeeIdRouteValue);
        }


        public IActionResult Index()
        {
            return _repository.GetStudents() != null ? View(_repository.GetStudents().Select(s =>
            {
                s.EncryptedRollNo = protector.Protect(s.RollNo.ToString());
                return s;
            })) : Problem("There is no Students!");
        }

        public IActionResult Details(string? id)
        {
            int decryptedId = Convert.ToInt32(protector.Unprotect(id));
            if (decryptedId == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(decryptedId);
            student.EncryptedRollNo = id;

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
        public IActionResult Edit(string? id)
        {
            int? decryptedId = Convert.ToInt32(protector.Unprotect(id));
            if (decryptedId == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(decryptedId ?? 0);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            int decryptedId = Convert.ToInt32(protector.Unprotect(id));
            if (decryptedId != student.RollNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _repository.UpdateStudentAsync(decryptedId, student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string? id)
        {
            int decryptedId = Convert.ToInt32(protector.Unprotect(id));
            if (decryptedId == null || _repository.GetStudents() == null)
            {
                return NotFound();
            }

            var student = _repository.GetStudentById(decryptedId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            int decryptedId = Convert.ToInt32(protector.Unprotect(id));
            if (_repository.GetStudents() == null)
            {
                return Problem("Entity set 'DatabaseContext.Students' is null.");
            }
            var student = _repository.GetStudentById(decryptedId);
            if (student != null)
            {
                await _repository.DeleteStudentAsync(decryptedId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
