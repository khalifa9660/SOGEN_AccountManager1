
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface ITeamService
	{
		Task<Team> AddTeamAsync(TeamDTO teamDTO);

		Task<IEnumerable<Team>> GetAllTeamsWithoutUserId();

		Task<Team> GetTeamById(int teamId);

		Task<IEnumerable<Team>> GetTeamsByChampionshipId(int championshipId);

		Task<IEnumerable<Team>> GetTeamsByUserId(int userId);

		Task<Team> EditTeamAsync(Team team);

		Task<bool> deleteTeamAsync(Team team);
    }
}