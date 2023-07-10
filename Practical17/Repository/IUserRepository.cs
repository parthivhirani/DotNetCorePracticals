using Practical17.Enums;
using Practical17.Models;
using Practical17.ViewModel;

namespace Practical17.Repository
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(RegisterViewModel user);

        Task<UserLoginStatus> LoginUserAsync(LoginViewModel model);

        Task<User> GetUserByEmailAsync(string email);

        List<string> GetUserRoles(int id);
    }
}
