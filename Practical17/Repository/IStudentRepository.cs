using Practical17.Models;

namespace Practical17.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetStudents();

        Student GetStudentById(int id);

        Task AddStudentAsync(Student student);

        Task<bool> UpdateStudentAsync(int id, Student student);

        Task DeleteStudentAsync(int id);

        bool StudentExists(int id);
    }
}
