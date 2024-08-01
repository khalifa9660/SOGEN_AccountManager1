
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Implementation;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.PlayerService
{
    public class PlayerService : IPlayerService {

        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

    public async Task<Player> AddPlayer(Player player)
    {
        return await _playerRepository.AddPlayer(player);
    }    

    public async Task<IEnumerable<Player>> GetPlayersByUserId(int userId)
    {
        return await _playerRepository.GetPlayersByUserId(userId);
    }

    public async Task<Player> EditPlayerAsync(Player player)
    {
        var editPlayer = await _playerRepository.FindPlayerByIdAsync(player.Id);

        if (editPlayer != null)
        {
            editPlayer.Name = player.Name;
            editPlayer.Age = player.Age;
            editPlayer.Number = player.Number;
            editPlayer.Position = player.Position;
            editPlayer.Photo = player.Photo;

            await _playerRepository.UpdatePlayerAsync(editPlayer); // Assurez-vous que cette méthode existe dans votre PlayerRepository
        }

        return editPlayer;
    }


        public async Task<bool> DeletePlayersAsync(IEnumerable<int> ids)
        {
            bool allDeleted = true;
            foreach (var id in ids)
            {
                var playerToDelete = await _playerRepository.FindPlayerByIdAsync(id);
                if (playerToDelete != null)
                {
                    await _playerRepository.DeletePlayerAsync(playerToDelete.Id); // Utilisez la méthode de suppression unique
                }
                else
                {
                    // Si un joueur avec l'ID spécifié n'est pas trouvé, marque la suppression comme échouée
                    allDeleted = false;
                }
            }

            return allDeleted;
        }

    }
}

        
        