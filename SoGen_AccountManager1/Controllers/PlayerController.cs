using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerRepository _playerRepository;

        private readonly ApplicationDbContext dbContext;

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
        public async Task<IActionResult> AddPlayer(CreateRequestPlayerDTO req)
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

        [HttpGet("GetEditPlayer")]
        public async Task<IActionResult> GetEditPlayer(Guid id)
        {
            var player = _playerRepository.GetPlayerToEdit(id);

            return Ok(player);
        }

        [HttpPut("EditPlayer")]
        public async Task<IActionResult> EditPlayer(CreateRequestPlayerDTO req)
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

            await _playerRepository.EditPlayer(player);

            return Ok(response);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Player player)
        {

            var isDeleted = await _playerRepository.DeletePlayer(player);

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
