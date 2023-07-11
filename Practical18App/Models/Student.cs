using System.Reflection;

namespace Practical18App.Models
{
    public class Student
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }
    }
}
