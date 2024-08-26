using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDTO playerDTO)
        {
            try
            {
                var player = await _playerService.AddPlayerAsync(playerDTO);
                return Ok(player);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetAllPlayers")]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var allPlayers = await _playerService.GetAllPlayers();
                return Ok(allPlayers);
            }
            catch (Exception ex)
            {
                
                return NoContent();
            }
        }


        [HttpGet("GetPlayersByUserId/{userId}")]
        public async Task<IActionResult> GetPlayersByUserId(int userId)
        {
            var playersByUserId = await _playerService.GetPlayersByUserId(userId);

            if (playersByUserId != null && playersByUserId.Any())
            {
                // Si des joueurs ont été trouvés, retourner la liste
                return Ok(playersByUserId);
            }
            else
            {
                // Si aucun joueur n'a été trouvé, retourner une réponse NoContent ou NotFound
                return NoContent(); // ou NotFound();
            }
        }

        [HttpGet("GetPlayerById/{playerId}")]
        public async Task<IActionResult> GetPlayerById(int playerId)
        {
            var players = await _playerService.GetPlayerById(playerId);

            if (players != null)
            {
                // Si des joueurs ont été trouvés, retourner la liste
                return Ok(players);
            }
            else
            {
                // Si aucun joueur n'a été trouvé, retourner une réponse NoContent ou NotFound
                return NoContent(); // ou NotFound();
            }
        }


        [HttpGet("GetPlayerByTeamId/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeamId(int teamId)
        {
            var players = await _playerService.GetPlayersByTeamId(teamId);

            if (players != null)
            {
                // Si des joueurs ont été trouvés, retourner la liste
                return Ok(players);
            }
            else
            {
                // Si aucun joueur n'a été trouvé, retourner une réponse NoContent ou NotFound
                return NoContent(); // ou NotFound();
            }
        }



        [HttpPut("EditPlayer")]
        public async Task<IActionResult> EditPlayer(int playerId, [FromBody] PlayerDTO playerDTO)
        {
            // Vérifie si le PlayerDTO est valide
            if (playerDTO == null || playerId != playerDTO.Id)
            {
                return BadRequest("Invalid player data.");
            }

            // Recherche du joueur existant dans la base de données par son ID
            var existingPlayer = await _playerService.GetPlayerById(playerId);
            
            if (existingPlayer == null)
            {
                return NotFound($"Player with ID {playerId} not found.");
            }

            // Met à jour les propriétés du joueur existant avec les nouvelles valeurs
                existingPlayer.Name = playerDTO.Name;
                existingPlayer.Age = playerDTO.Age;
                existingPlayer.Number = playerDTO.Number;
                existingPlayer.Position = playerDTO.Position;
                existingPlayer.Nationality = playerDTO.Nationality;
                existingPlayer.Photo = playerDTO.Photo;

            // Appel du service pour enregistrer les modifications
            var editedPlayer = await _playerService.EditPlayerAsync(existingPlayer);

            if (editedPlayer != null)
            {
                return Ok(editedPlayer);
            }
            else
            {
                // Si l'édition échoue, retourne un statut 500 (Internal Server Error)
                return StatusCode(500, "An error occurred while updating the player.");
            }
        }



        [HttpDelete("Delete/{ids}")]
        public async Task<IActionResult> DeleteMultiple(string ids)
        {
            // Séparer les identifiants et les convertir en Guid
            var idList = ids.Split(',').Select(int.Parse).ToList();

            bool isDeleted = await _playerService.DeletePlayersAsync(idList);

            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound("One or more players not found.");
            }
        }

    }
}
