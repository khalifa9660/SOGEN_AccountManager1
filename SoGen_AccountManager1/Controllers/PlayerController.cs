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


        [HttpGet("GetAllPlayers")]
        public async Task<IActionResult> GetAllPlayers()
        {
            var players = await _playerRepository.GetPlayers();

            return Ok(players);
        }


        [HttpPost("AddPlayer")]
        public async Task<IActionResult> AddPlayer(UpdatePlayerDTO req)
        {
            //Map DTO to Domain Model
            var player = new Player
            {
                Name = req.Name,
                Age = req.Age,
                Number = req.Number,
                Position = req.Position,
                Photo = req.Photo
            };


            //Domain model to Dto
            var response = new PlayerDto
            {
                Name = player.Name,
                Age = player.Age,
                Number = player.Number,
                Position = player.Position,
                Photo = player.Photo
            };

           await _playerRepository.AddPlayer(player);

            return Ok(response);
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


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            bool isDeleted = await _playerRepository.DeletePlayer(id);

            if (isDeleted)
            {
                return Ok(); 
            }
            else
            {
                return NotFound();
            }
        }

    }
}
