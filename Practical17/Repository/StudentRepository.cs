using Practical17.DataContext;
using Practical17.Models;

namespace Practical17.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            if (StudentExists(id))
            {
                _context.Students.Remove(_context.Students.First(x => x.RollNo == id));
                await _context.SaveChangesAsync();
            }
        }

        public Student GetStudentById(int id)
        {
            if (!StudentExists(id))
            {
                return null;
            }
            return _context.Students.First(x => x.RollNo == id);
        }

        public List<Student> GetStudents()
        {
            return _context.Students.ToList();
        }

        public bool StudentExists(int id)
        {
            return _context.Students.Any(x => x.RollNo == id);
        }

        public async Task<bool> UpdateStudentAsync(int id, Student student)
        {
            Student temp = _context.Students.FirstOrDefault(x => x.RollNo == id);
            if (temp != null)
            {
                temp.FirstName = student.FirstName;
                temp.MiddleName = student.MiddleName;
                temp.LastName = student.LastName;
                temp.DOB = student.DOB;
                temp.Standard = student.Standard;
                temp.Address = student.Address;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
