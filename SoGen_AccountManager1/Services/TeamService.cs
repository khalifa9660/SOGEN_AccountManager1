using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.TeamService
{
    public class TeamService : ITeamService {

        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository )
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> AddTeamAsync(TeamDTO teamDTO)
        {
            var team = new Team
            {
                Name = teamDTO.Name,
                Photo = teamDTO.Photo,
                Founded = teamDTO.Founded
            };

            return await _teamRepository.AddTeam(team);
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _teamRepository.GetAllTeams();
        }

        public async Task<Team> GetTeamByid(int teamId)
        {
            return await _teamRepository.GetTeamById(teamId);
        }
    }
    
}