using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IPlayerRepository
	{
        Task<Player> AddPlayerAsync(Player player);

        Task<IEnumerable<Player>> GetAllPlayersWithoutUserId();

        Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

        Task<IEnumerable<Player>> GetPlayersByTeamId(int teamId);

        Task<Player> GetPlayerById(int playerId);

        Task<Player> FindPlayerByIdAsync(int id);

		Task UpdatePlayerAsync(Player player);

        Task DeletePlayerAsync(int id);

        Task DeletePlayersAsync(IEnumerable<int> ids);

    }
}