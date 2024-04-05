using System;
using System.Collections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Player> AddPlayer(Player player)
        {
            await dbContext.Players.AddAsync(player);
            await dbContext.SaveChangesAsync();

            return player;
        }

        public async Task<IdentityUser> FindUserById(Guid userId)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());
        }

        public async Task<IEnumerable<Player>> GetPlayersByUserId(Guid userId)
        {
            return await dbContext.Players.Where(p => p.User_id == userId).ToListAsync();
        }

        public async Task<Player> EditPlayer(Player player)
        {
           var editPlayer = await dbContext.Players.FindAsync(player.Id);

            if(editPlayer is not null)
            {
                editPlayer.Name = player.Name;
                editPlayer.Age = player.Age;
                editPlayer.Number = player.Number;
                editPlayer.Position = player.Position;
                editPlayer.Photo = player.Photo;

                await dbContext.SaveChangesAsync();
            }

            return editPlayer;
        }

        public async Task<bool> DeletePlayers(IEnumerable<Guid> ids)
        {
            bool allDeleted = true;
            foreach (var id in ids)
            {
                var playerToDelete = await dbContext.Players.FindAsync(id);
                if (playerToDelete != null)
                {
                    dbContext.Players.Remove(playerToDelete);
                }
                else
                {
                    // Si un joueur avec l'ID spécifié n'est pas trouvé, marque la suppression comme échouée
                    allDeleted = false;
                }
            }

            if (allDeleted)
            {
                // Faites l'appel SaveChangesAsync ici pour appliquer toutes les suppressions en une seule transaction
                await dbContext.SaveChangesAsync();
            }

            return allDeleted;
        }

    }
}

