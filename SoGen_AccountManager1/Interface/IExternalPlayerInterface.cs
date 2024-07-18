using Microsoft.AspNetCore.Identity;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface
{
	public interface IPlayerRepository
	{
		Task<Player> AddPlayer(Player player);

		Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

        Task<Player> EditPlayer(Player player);

		Task<bool> DeletePlayers(IEnumerable<int> ids);
        Task<int> FindUserById(int userId);
    }
}

