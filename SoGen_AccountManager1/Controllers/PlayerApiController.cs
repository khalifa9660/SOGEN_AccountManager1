using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.Domain;
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


        // [HttpGet("GetPlayersByUser")]
        // public async Task<IActionResult> GetPlayersByUserId(int userId)
        // {
        //     var playersByUserId = await _playerService.GetPlayersByUserId(userId);

        //     if (playersByUserId != null && playersByUserId.Any())
        //     {
        //         // Si des joueurs ont été trouvés, retourner la liste
        //         return Ok(playersByUserId);
        //     }
        //     else
        //     {
        //         // Si aucun joueur n'a été trouvé, retourner une réponse NoContent ou NotFound
        //         return NoContent(); // ou NotFound();
        //     }
        // }

        [HttpGet("GetPlayerById")]
        public async Task<IActionResult> GetPlayersById(int playerId)
        {
            var players = await _playerService.GetPlayersById(playerId);

            if (players != null && players.Any())
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
        public async Task<IActionResult> EditPlayer(PlayerDTO req)
        {
            var player = new Player
            {
                Name = req.Name,
                Age = req.Age,
                Number = req.Number,
                Position = req.Position,
                Nationality = req.Nationality,
                Photo = req.Photo
            };

            var editedPlayer = await _playerService.EditPlayerAsync(player);

            if (editedPlayer == null)
            {
                return NotFound();
            }

        
            var response = new PlayerDTO
            {
                Name = editedPlayer.Name,
                Age = editedPlayer.Age,
                Number = editedPlayer.Number,
                Position = editedPlayer.Position,
                Nationality = editedPlayer.Nationality,
                Photo = editedPlayer.Photo
            };

            return Ok(response);
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
