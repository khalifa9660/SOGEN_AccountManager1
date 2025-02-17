using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IChampionshipRepository
	{
		Task<Championship> AddChampionship(Championship championship);

		Task<IEnumerable<Championship>> GetAllChampionshipsAsync();

		Task<IEnumerable<Championship>> GetAllChampionshipsWithoutUserIdAsync();

		Task UpdateChampionshipAsync(Championship championship);

		Task<Championship> GetChampionshipById(int ChampionshipId);

		Task<IEnumerable<Championship>> GetChampionshipByUserId(int userId);

		Task<bool> DeleteChampionship(int Id);
    }
}