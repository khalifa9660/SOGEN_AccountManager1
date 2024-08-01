
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Models.DTO;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IUserService
	{
        Task<User> AddUserAsync(User user);
        Task<(User User, string Token)> RegisterUserAsync(UserRegisterDTO userRegisterDTO);
        Task<bool> CheckPasswordAsync(string email, string password);
        Task<(User User, string Token)> LoginAsync(string email, string password);
        Task<bool> CheckIfUserExistsAsync(string email);
         Task<User> FindByEmailAsync(string email);
    }
}