using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }


        [HttpGet("GetPlayersByUser")]
        public async Task<IActionResult> GetPlayers()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Récupérez l'ID de l'utilisateur connecté à partir des claims
                var userIdString = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (userIdString != null && Guid.TryParse(userIdString, out Guid userId))
                {
                    var user = await _playerRepository.FindUserById(userId);
                    if (user != null)
                    {
                        // Récupérez les joueurs associés à l'ID de l'utilisateur
                        var players = await _playerRepository.GetPlayersByUserId(userId);

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
            else
            {
                return Unauthorized("User is not authenticated.");
            }
        }



        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(UpdatePlayerDTO req)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Récupérez l'ID de l'utilisateur connecté à partir des claims
                var userIdString = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;

                if (userIdString != null && Guid.TryParse(userIdString, out Guid userId))
                {
                    // Assurez-vous que l'utilisateur existe dans la base de données
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
                            User_id = userId // Utilisez l'ID de l'utilisateur connecté
                        };

                        // Domain model to Dto
                        var response = new PlayerDto
                        {
                            Name = player.Name,
                            Age = player.Age,
                            Number = player.Number,
                            Position = player.Position,
                            Photo = player.Photo,
                        };

                        await _playerRepository.AddPlayer(player);
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
        public async Task<IActionResult> EditPlayer(UpdatePlayerDTO req)
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

            var editedPlayer = await _playerRepository.EditPlayer(player);

            if (editedPlayer == null)
            {
                return NotFound();
            }

        
            var response = new PlayerDto
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
            var idList = ids.Split(',').Select(Guid.Parse).ToList();

            bool isDeleted = await _playerRepository.DeletePlayers(idList);

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
