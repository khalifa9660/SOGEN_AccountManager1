using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IPlayerRepository
	{
        Task<Player> AddPlayer(Player player);

        Task<IEnumerable<Player>> GetPlayersByUserId(int userId);

        Task<Player> FindPlayerByIdAsync(int id);

		Task UpdatePlayerAsync(Player player);

        Task<Player> EditPlayer(Player player);

        Task DeletePlayerAsync(int id);

        Task DeletePlayersAsync(IEnumerable<int> ids);

        Task<IEnumerable<Player>> FindPlayersById(int id);

    }
}