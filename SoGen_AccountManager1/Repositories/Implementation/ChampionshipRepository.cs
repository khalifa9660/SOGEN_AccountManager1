
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class ChampionshipRepository : IChampionshipRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChampionshipRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Championship> AddChampionship(Championship championship)
        {
            await _dbContext.Championships.AddAsync(championship);
            await _dbContext.SaveChangesAsync();

            return championship;
        }

        public async Task UpdateChampionshipAsync(Championship championship)
        {
            _dbContext.Championships.Update(championship);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Championship>> GetAllChampionshipsAsync()
        {
            return await _dbContext.Championships.ToListAsync();
        }

        public async Task<Championship> GetChampionshipById(int ChampionshipId)
        {
            return await _dbContext.Championships.FirstOrDefaultAsync(u => u.Id == ChampionshipId);
        }

        public async Task<IEnumerable<Championship>> GetChampionshipByUserId(int userId)
        {
            return await _dbContext.Championships.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task DeleteChampionshio(Championship championship)
        {
             _dbContext.Championships.Remove(championship);
             await _dbContext.SaveChangesAsync();
        }

    }
}

