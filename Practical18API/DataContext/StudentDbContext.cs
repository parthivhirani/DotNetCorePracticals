using Microsoft.EntityFrameworkCore;
using Practical18API.Models;

namespace Practical18API.DataContext
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options): base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
    }
}
