
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IUserService
	{
        Task<bool> CreateUserAsync(User user, string password);
    }
}