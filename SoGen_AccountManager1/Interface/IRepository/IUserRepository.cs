using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IUserRepository
	{

        Task<User> AddUser(User user);

        Task<User> FindUserByEmail(string email);

        Task<User> FindUserById(int userId);

    }
}