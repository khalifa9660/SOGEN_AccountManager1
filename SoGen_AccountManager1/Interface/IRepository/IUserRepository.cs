using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IUserRepository
	{

        Task<User> AddUserAsync(User user);

        Task<User> FindUserByEmailAsync(string email);

        Task<User> FindUserByIdAsync(int userId);

        Task<User> CreateUserAsync(UserDTO userDTO);

    }
}