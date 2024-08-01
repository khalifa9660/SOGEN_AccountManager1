using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface.IRepository
{
	public interface ITeamRepository
	{
		Task<Team> AddTeam(Team team);

		Task<Team> FindTeamById(int TeamId);
    }
}