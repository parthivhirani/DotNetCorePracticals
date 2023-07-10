using Microsoft.EntityFrameworkCore;
using Practical17.Models;
using Practical17.ViewModel;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Practical17.DataContext
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "admin",
                },
                new Role
                {
                    Id = 2,
                    RoleName = "user",
                }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@gmail.com",
                    MobileNumber = "1111111111",
                    Password = "admin@123"
                },
                new User
                {
                    UserId = 2,
                    FirstName = "parthiv",
                    LastName = "hirani",
                    Email = "parthiv@gmail.com",
                    MobileNumber = "2222222222",
                    Password = "parthiv@123"
                }
                );

            modelBuilder.Entity<UserRole>().HasKey(k => new { k.UserId, k.RoleId });

            modelBuilder.Entity<UserRole>()
                        .HasOne(u => u.Users)
                        .WithMany(u => u.UserRoles)
                        .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Roles)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    UserId = 1,
                    RoleId = 1
                },
                new UserRole
                {
                    UserId = 2,
                    RoleId = 2
                }
                );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    RollNo = 101,
                    FirstName = "Student1",
                    MiddleName = "Student1 Middle Name",
                    LastName = "Student1 Last Name",
                    DOB = DateTime.Now,
                    Standard = 6,
                    Address = "Rajkot"
                },
                new Student
                {
                    RollNo = 102,
                    FirstName = "Student2",
                    MiddleName = "Student2 Middle Name",
                    LastName = "Student2 Last Name",
                    DOB = DateTime.Now,
                    Standard = 8,
                    Address = "Ahmedabad"
                },
                new Student
                {
                    RollNo = 103,
                    FirstName = "Student3",
                    MiddleName = "Student3 Middle Name",
                    LastName = "Student3 Last Name",
                    DOB = DateTime.Now,
                    Standard = 3,
                    Address = "Jamnagar"
                }
            );
        }

        public DbSet<LoginViewModel> LoginViewModels { get; set; }
        public DbSet<RegisterViewModel> RegisterViewModels { get; set; }
    }
}
