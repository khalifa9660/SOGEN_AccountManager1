
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface IChampionshipService
	{
		Task<Championship> AddChampionshipAsync(ChampionshipDTO championshipDTO);

		Task<IEnumerable<Championship>> GetAllChampionships();

		Task<Championship> GetChampionshipById(int championshipById);
    }
}