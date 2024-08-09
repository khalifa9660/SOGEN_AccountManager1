
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IService
{
	public interface ITeamService
	{
		Task<Team> AddTeamAsync(TeamDTO teamDTO);

		Task<IEnumerable<Team>> GetAllTeams();

		Task<Team> GetTeamByid(int teamId);
    }
}