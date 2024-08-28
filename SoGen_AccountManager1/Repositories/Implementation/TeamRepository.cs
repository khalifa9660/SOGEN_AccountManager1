
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TeamRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Team> AddTeam(Team team)
        {
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            return team;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _dbContext.Teams.ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetAllTeamsWithoutUserId()
        {
            return await _dbContext.Teams.Where(p => p.UserId == null).ToListAsync();
        }

        public async Task<Team> GetTeamById(int TeamId)
        {
            return await _dbContext.Teams.FirstOrDefaultAsync(u => u.Id == TeamId);
        }

        public async Task<IEnumerable<Team>> GetTeamByUserId(int userId)
        {
            return await _dbContext.Teams.Where(u => u.UserId == userId).ToListAsync();
        }


        public async Task<IEnumerable<Team>> GetTeamByChampionshipId(int championshipId)
        {
            return await _dbContext.Teams.Where(u => u.ChampionshipId == championshipId).ToListAsync();
        }


        public async Task UpdateTeamAsync(Team team)
        {
            _dbContext.Teams.Update(team);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTeam(Team team)
        {
            _dbContext.Remove(team);
            await _dbContext.SaveChangesAsync();
        }

    }
}

