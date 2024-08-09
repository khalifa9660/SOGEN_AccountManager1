
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class ChampionshipRepository : IChampionshipRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ChampionshipRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Championship> AddChampionship(Championship championship)
        {
            await dbContext.Championships.AddAsync(championship);
            await dbContext.SaveChangesAsync();

            return championship;
        }

        public async Task<IEnumerable<Championship>> GetAllChampionshipsAsync()
        {
            return await dbContext.Championships.ToListAsync();
        }

        public async Task<Championship> GetChampionshipById(int ChampionshipId)
        {
            return await dbContext.Championships.FirstOrDefaultAsync(u => u.Id == ChampionshipId);
        }

    }
}

