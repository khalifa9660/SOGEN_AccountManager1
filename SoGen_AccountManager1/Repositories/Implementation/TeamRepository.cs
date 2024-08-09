
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TeamRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Team> AddTeam(Team team)
        {
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();

            return team;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await dbContext.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(int TeamId)
        {
            return await dbContext.Teams.FirstOrDefaultAsync(u => u.Id == TeamId);
        }

    }
}

