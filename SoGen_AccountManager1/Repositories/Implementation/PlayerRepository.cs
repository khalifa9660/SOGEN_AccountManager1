using System;
using System.Collections;
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

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            var players = await dbContext.Players.ToListAsync();

            return players.ToArray();
        }


        public async Task<Player> GetPlayerToEdit(Guid id)
        {
            var player = await dbContext.Players.FindAsync(id);

            return player;
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

        public async Task<bool> DeletePlayer(Player player)
        {
            var playerToDelete = await dbContext.Players.FindAsync(player.Id);

            if (playerToDelete != null)
            {
                dbContext.Players.Remove(playerToDelete);
                await dbContext.SaveChangesAsync();
                return true; // Le joueur a été supprimé avec succès
            }

            return false; // Le joueur n'a pas été trouvé dans la base de données
        }


    }
}

