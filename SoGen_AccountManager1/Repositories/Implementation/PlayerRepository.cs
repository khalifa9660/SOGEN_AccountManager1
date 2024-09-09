using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> AddPlayerAsync(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            return player;
        }

        public async Task<IEnumerable<Player>> GetAllPlayersWithoutUserId()
        {
            return await _dbContext.Players.Where(p => p.UserId == null).ToListAsync();
        }

        
        public async Task<Player> FindPlayerByIdAsync(int id)
        {
            return await _dbContext.Players.FindAsync(id);
        }

        public async Task UpdatePlayerAsync(Player player)
        {
            _dbContext.Players.Update(player);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByUserId(int userId)
        {
            return await _dbContext.Players.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamId(int teamId)
        {
            return await _dbContext.Players.Where(p => p.TeamId == teamId).ToListAsync();
        }

        public async Task<Player> GetPlayerById(int playerId)
        {
            return await _dbContext.Players.FirstOrDefaultAsync(p => p.Id == playerId);
        }

        public async Task DeletePlayerAsync(int id) 
        {
            var player = await FindPlayerByIdAsync(id);
            if (player != null)
            {
                _dbContext.Players.Remove(player);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePlayersAsync(IEnumerable<int> ids)
        {
            var players = _dbContext.Players.Where(p => ids.Contains(p.Id));
            _dbContext.Players.RemoveRange(players);
            await _dbContext.SaveChangesAsync();
        }
    }
}

