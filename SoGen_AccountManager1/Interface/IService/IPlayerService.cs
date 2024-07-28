
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IPlayerService
	{
        Task<Player> AddPlayer(Player player);

		Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

        Task<Player> EditPlayer(Player player);

		Task<bool> DeletePlayers(IEnumerable<int> ids);
    }
}