
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IPlayerService
	{
		Task<Player> AddPlayerAsync(PlayerDTO playerDTO);

		Task<IEnumerable<Player>> GetAllPlayersWithoutUserId();

		Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

		Task<IEnumerable<Player>> GetPlayersByTeamId(int teamId);

		Task<Player> GetPlayerById(int playerId);

		Task<Player> EditPlayerAsync(Player player);

		 Task<bool> DeletePlayersAsync(IEnumerable<int> ids);
    }
}