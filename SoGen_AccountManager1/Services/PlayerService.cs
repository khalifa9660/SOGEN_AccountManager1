
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Implementation;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Services.PlayerService
{
    public class PlayerService : IPlayerService {

        private readonly IPlayerRepository _playerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PlayerService(IPlayerRepository playerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _playerRepository = playerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Player> AddPlayerAsync(PlayerDTO playerDTO)
        {
            // Récupérer l'ID de l'utilisateur connecté à partir du token JWT (sous forme de string)
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Vérifier si l'ID utilisateur est valide et peut être converti en entier
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out var userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated or has an invalid ID.");
            }

            var player = new Player
            {
                Name = playerDTO.Name,
                Age = playerDTO.Age,
                Number = playerDTO.Number,
                Position = playerDTO.Position,
                Nationality = playerDTO.Nationality,
                Photo = playerDTO.Photo,
                TeamId = playerDTO.TeamId,
                UserId = userId
            };

            return await _playerRepository.AddPlayerAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllPlayersWithoutUserId()
        {
            return await _playerRepository.GetAllPlayersWithoutUserId();
        }

        

    public async Task<IEnumerable<Player>> GetPlayersByUserId(int userId)
    {
        return await _playerRepository.GetPlayersByUserId(userId);
    }

    public async Task<IEnumerable<Player>> GetPlayersByTeamId(int teamId)
    {
        return await _playerRepository.GetPlayersByTeamId(teamId);
    }

        public async Task<Player> GetPlayerById(int playerId)
        {
            return await _playerRepository.GetPlayerById(playerId);
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

        
        