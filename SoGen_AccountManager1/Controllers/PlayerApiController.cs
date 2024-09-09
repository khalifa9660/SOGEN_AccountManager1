using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


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
            if (playerDTO == null)
            {
                return BadRequest("Invalid player data.");
            }

            var player = await _playerService.AddPlayerAsync(playerDTO);
            return Ok(player);
        }


        [HttpGet("GetAllPlayers")]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var allPlayers = await _playerService.GetAllPlayersWithoutUserId();
                return Ok(allPlayers);
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }


        [HttpGet("GetPlayersByUserId/{userId}")]
        public async Task<IActionResult> GetPlayersByUserId(int userId)
        {
            var playersByUserId = await _playerService.GetPlayersByUserId(userId);

            if (playersByUserId != null && playersByUserId.Any())
            {
                return Ok(playersByUserId);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetPlayerById/{playerId}")]
        public async Task<IActionResult> GetPlayerById(int playerId)
        {
            var players = await _playerService.GetPlayerById(playerId);

            if (players != null)
            {
                return Ok(players);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpGet("GetPlayerByTeamId/{teamId}")]
        public async Task<IActionResult> GetPlayersByTeamId(int teamId)
        {
            var players = await _playerService.GetPlayersByTeamId(teamId);

            if (players != null)
            {
                return Ok(players);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("EditPlayer")]
        public async Task<IActionResult> EditPlayer(PlayerDTO playerDTO)
        {
            if (playerDTO == null)
            {
                return BadRequest("Invalid player data.");
            }

            // Recherche du joueur existant dans la base de données par son ID
            var existingPlayer = await _playerService.GetPlayerById(playerDTO.Id);
            
            if (existingPlayer == null)
            {
                return NotFound($"Player with ID not found.");
            }

            // Met à jour les propriétés du joueur existant avec les nouvelles valeurs
                existingPlayer.Name = playerDTO.Name;
                existingPlayer.Age = playerDTO.Age;
                existingPlayer.Number = playerDTO.Number;
                existingPlayer.Position = playerDTO.Position;
                existingPlayer.Nationality = playerDTO.Nationality;
                existingPlayer.Photo = playerDTO.Photo;

            var editedPlayer = await _playerService.EditPlayerAsync(existingPlayer);

            if (editedPlayer != null)
            {
                return Ok(editedPlayer);
            }
            else
            {
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
                return StatusCode(500, "An error occurred while deleting one or many players.");
            }
        }

    }
}
