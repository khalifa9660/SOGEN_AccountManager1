using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface IChampionshipRepository
	{
		Task<Championship> AddChampionship(Championship championship);

		Task<Championship> FindChampionshipById(int ChampionshipId);
    }
}