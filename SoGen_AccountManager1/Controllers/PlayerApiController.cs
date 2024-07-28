using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;
using System.Security.Claims;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        private readonly IUserRepository _userRepository;


        public PlayerController(IPlayerService playerService, IUserRepository userRepository)
        {
            _playerService = playerService;
            _userRepository = userRepository;
        }


        [HttpGet("GetPlayersByUser")]
    public async Task<IActionResult> GetPlayers()
    {
        // Extraire l'ID de l'utilisateur à partir des claims du token
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (userIdString != null && int.TryParse(userIdString, out int userId))
        {
            var user = await _userRepository.FindUserById(userId);
            if (user != null)
            {
                // Récupérez les joueurs associés à l'ID de l'utilisateur
                var players = await _playerService.GetPlayersByUserId(userId);
                return Ok(players);
            }
            else
            {
                return NotFound("User not found.");
            }
        }
        else
        {
            return BadRequest("Could not parse the user ID.");
        }
    }



        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(PlayerDTO req)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Récupérez l'ID de l'utilisateur connecté à partir des claims
                var userIdString = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (userIdString != null && int.TryParse(userIdString, out int userId))
                {
                    if (userIdString != null)
                    {
                        // Map DTO to Domain Model en utilisant l'ID récupéré
                        var player = new Player
                        {
                            Name = req.Name,
                            Age = req.Age,
                            Number = req.Number,
                            Position = req.Position,
                            Photo = req.Photo,
                            User_id = userId
                        };

                        // Domain model to Dto
                        var response = new PlayerDTO
                        {
                            Name = player.Name,
                            Age = player.Age,
                            Number = player.Number,
                            Position = player.Position,
                            Photo = player.Photo,
                        };

                        await _playerService.AddPlayer(player);
                        return Ok(response);
                    }
                    else
                    {
                        return NotFound("User not found.");
                    }
                }
                else
                {
                    return BadRequest("Could not parse the user ID.");
                }
            }
            else
            {
                return Unauthorized("User is not authenticated.");
            }
        }



        [HttpPut("EditPlayer")]
        public async Task<IActionResult> EditPlayer(PlayerDTO req)
        {
            var player = new Player
            {
                Id = req.Id,
                Name = req.Name,
                Age = req.Age,
                Number = req.Number,
                Position = req.Position,
                Photo = req.Photo
            };

            var editedPlayer = await _playerService.EditPlayer(player);

            if (editedPlayer == null)
            {
                return NotFound();
            }

        
            var response = new PlayerDTO
            {
                Id = editedPlayer.Id,
                Name = editedPlayer.Name,
                Age = editedPlayer.Age,
                Number = editedPlayer.Number,
                Position = editedPlayer.Position,
                Photo = editedPlayer.Photo
            };

            return Ok(response);
        }


        [HttpDelete("Delete/{ids}")]
        public async Task<IActionResult> DeleteMultiple(string ids)
        {
            // Séparer les identifiants et les convertir en Guid
            var idList = ids.Split(',').Select(int.Parse).ToList();

            bool isDeleted = await _playerService.DeletePlayers(idList);

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
