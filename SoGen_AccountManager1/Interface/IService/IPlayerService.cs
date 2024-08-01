
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IPlayerService
	{
		Task<Player> AddPlayer(Player player);

		Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

		Task<Player> EditPlayerAsync(Player player);

		 Task<bool> DeletePlayersAsync(IEnumerable<int> ids);
    }
}