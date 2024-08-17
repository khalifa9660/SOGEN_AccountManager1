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
                Founded = teamDTO.Founded,
                ChampionshipId = teamDTO.ChampionshipId,
                UserId = teamDTO.UserId
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


        public async Task<IEnumerable<Team>> GetTeamsByChampionshipId(int championshipId)
        {
            return await _teamRepository.GetTeamByChampionshipId(championshipId);
        } 

        public async Task<IEnumerable<Team>> GetTeamsByUserId(int userId)
        {
            return await _teamRepository.GetTeamByUserId(userId);
        } 

        public async Task<Team> EditTeamAsync(Team team)
        {
            var teamToUpdate = await _teamRepository.GetTeamById(team.Id);

            if(teamToUpdate != null)
            {
                teamToUpdate.Name = team.Name;
                teamToUpdate.Photo = team.Photo;

                await _teamRepository.UpdateTeamAsync(teamToUpdate);

            }

            return teamToUpdate;
        }

        public async Task<bool> deleteTeamAsync(Team team)
        {
            bool teamDeleted = true;
            var teamToDelete = await _teamRepository.GetTeamById(team.Id);

            if(teamToDelete != null)
            {
                await _teamRepository.DeleteTeam(teamToDelete);
            }
            else
            {
                teamDeleted = false;
            }

            return teamDeleted;
        }
    }
    
}