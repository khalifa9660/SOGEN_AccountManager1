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

        public async Task<Player> AddPlayer(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            return player;
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
            return await _dbContext.Players.Where(p => p.User_id == userId).ToListAsync();
        }

        public async Task<Player> EditPlayer(Player player)
        {
           var editPlayer = await _dbContext.Players.FindAsync(player.Id);

            if(editPlayer is not null)
            {
                editPlayer.Name = player.Name;
                editPlayer.Age = player.Age;
                editPlayer.Number = player.Number;
                editPlayer.Position = player.Position;
                editPlayer.Photo = player.Photo;

                await _dbContext.SaveChangesAsync();
            }

            return editPlayer;
        }

        public async Task DeletePlayerAsync(int id) // Implémentation de la suppression unique
        {
            var player = await FindPlayerByIdAsync(id);
            if (player != null)
            {
                _dbContext.Players.Remove(player);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePlayersAsync(IEnumerable<int> ids) // Méthode existante pour supprimer plusieurs joueurs
        {
            var players = _dbContext.Players.Where(p => ids.Contains(p.Id));
            _dbContext.Players.RemoveRange(players);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> FindPlayersById(int id)
        {
            return await _dbContext.Players.Where(p => p.Id == id ).ToListAsync();
        }
    }
}

