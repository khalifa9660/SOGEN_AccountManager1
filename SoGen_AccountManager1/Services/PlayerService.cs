
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.PlayerService
{
    public class PlayerService : IPlayerService {


        public PlayerService( )
        {
        }
    public async Task<IEnumerable<Player>> GetPlayersByUserId(int userId)
        {
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

        public async Task<bool> DeletePlayers(IEnumerable<int> ids)
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

        
        