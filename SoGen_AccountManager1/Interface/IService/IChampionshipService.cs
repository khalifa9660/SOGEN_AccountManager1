
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IChampionshipService
	{
		Task<Championship> AddChampionshipAsync(ChampionshipDTO championshipDTO);

		Task<IEnumerable<Championship>> GetAllChampionshipsWithoutUserIdAsync();

		Task<IEnumerable<Championship>> GetAllChampionshipsAsync();


		Task<Championship> GetChampionshipById(int championshipById);

		Task<IEnumerable<Championship>> GetChampionshipsByUserId(int userId);

		Task<Championship> EditChampionship(Championship championship);

		Task<bool> DeleteChampionshipAsync(Championship championship);
    }
}