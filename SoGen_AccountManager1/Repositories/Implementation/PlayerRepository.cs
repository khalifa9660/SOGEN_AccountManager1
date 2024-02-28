using System;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface;

namespace SoGen_AccountManager1.Repositories.Implementation
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Player> CreateAsync(Player player)
        {
            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();

            return player;
        }
    }
}

