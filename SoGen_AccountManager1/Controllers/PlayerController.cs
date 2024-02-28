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
        private readonly IPlayerRepository playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer(CreateRequestPlayerDTO req)
        {
            //Map DTO to Domain Model
            var player = new Player
            {
                Name = req.Name,
                LastName = req.LastName,
                Age = req.Age,
                Weight = req.Weight,
                Birth = req.Birth,
                Photo = req.Photo
            };

            await playerRepository.CreateAsync(player);

            //Domain model to Dto
            var response = new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                LastName = player.LastName,
                Age = player.Age,
                Weight = player.Weight,
                Birth = player.Birth,
                Photo = player.Photo
            };

            return Ok(response);
        }
    }
}
