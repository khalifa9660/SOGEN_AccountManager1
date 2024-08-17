using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IPlayerRepository
	{
        Task<Player> AddPlayerAsync(Player player);

        Task<IEnumerable<Player>> GetAllPlayers();

        Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

        Task<IEnumerable<Player>> GetPlayerByTeamId(int teamId);

        Task<Player> GetPlayersById(int playerId);

        Task<Player> FindPlayerByIdAsync(int id);

		Task UpdatePlayerAsync(Player player);

        Task DeletePlayerAsync(int id);

        Task DeletePlayersAsync(IEnumerable<int> ids);

    }
}