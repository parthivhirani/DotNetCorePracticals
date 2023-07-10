using Microsoft.EntityFrameworkCore;
using Practical17.DataContext;
using Practical17.Enums;
using Practical17.Models;
using Practical17.ViewModel;
using Practical17.Enums;

namespace Practical17.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly StudentDbContext _context;

        public UserRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task RegisterUserAsync(RegisterViewModel user)
        {
            _context.Users.Add(ChangeViewModelToModel(user));
            await _context.SaveChangesAsync();
            User temp = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (temp != null)
            {
                _context.UserRoles.Add(new UserRole() { RoleId = 2, UserId = temp.UserId });
                await _context.SaveChangesAsync();
            }

        }

        public async Task<UserLoginStatus> LoginUserAsync(LoginViewModel model)
        {
            if (model == null)
            {
                return UserLoginStatus.UserNull;
            }

            User user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return UserLoginStatus.UserNotFound;
            }

            if (user.Password != model.Password)
            {
                return UserLoginStatus.InvalidPassword;
            }
            else
            {
                return UserLoginStatus.LoginSuccess;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<string> GetUserRoles(int id)
        {
            var roles = _context.UserRoles
                .Where(x => x.UserId == id)
                .Join(_context.Roles, x => x.RoleId, x => x.Id, (user, role) => new { user.UserId, role.RoleName })
                .Select(x => x.RoleName);

            return roles.ToList();
        }

        private User ChangeViewModelToModel(RegisterViewModel model)
        {
            User user = new User()
            {
                UserId = model.Id,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
            };



            return user;
        }
    }
}
