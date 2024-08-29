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

    public class ChampionshipController : ControllerBase
    {
        private readonly IChampionshipService _championshipService;

        public ChampionshipController(IChampionshipService championshipService)
        {
            _championshipService = championshipService;
        }

        [HttpPost("AddChampionship")]
        public async Task<IActionResult> AddChampionship([FromBody] ChampionshipDTO championshipDTO)
        {
            try
            {
                var championship = await _championshipService.AddChampionshipAsync(championshipDTO);
                return Ok(championshipDTO);
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllChampionshipsWithoutUserId")]
        
        public async Task<IActionResult> GetAllChampionshipWithoutUserId()
        {
             try
             {
                var allChampionship = await _championshipService.GetAllChampionshipsWithoutUserIdAsync();
                return Ok(allChampionship);
             }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllChampionships")]
        
        public async Task<IActionResult> GetAllChampionship()
        {
             try
             {
                var allChampionship = await _championshipService.GetAllChampionshipsAsync();
                return Ok(allChampionship);
             }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetChampionshipById/{championshipId}")]
        public async Task<IActionResult> GetChampionshipById(int championshipId)
        {
            var championship = await _championshipService.GetChampionshipById(championshipId);

            if(championship != null)
            {
                return Ok(championship);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("GetChampionshipByUserId/{userId}")]
        public async Task<ActionResult> GetChampionshipByUserId(int userId)
        {
            var championships = await _championshipService.GetChampionshipsByUserId(userId);

            if(championships != null)
            {
                return Ok(championships);
            }
            else 
            {
                return NoContent();
            }
        }
        

        [HttpPost("EditChampionship")]
        public async Task<IActionResult> EditChampionship(ChampionshipDTO championshipDTO)
        {
            if(championshipDTO == null)
            {
                return BadRequest("Invalid League data");
            }

            var existingLeague = await _championshipService.GetChampionshipById(championshipDTO.Id);

            if(existingLeague == null)
            {
                return NotFound("Team with ID not found");
            }

            existingLeague.Name = championshipDTO.Name;
            existingLeague.Country = championshipDTO.Country;

            var editedTeam = await _championshipService.EditChampionship(existingLeague);

            if(editedTeam != null)
            {
                return Ok(editedTeam);
            }
            else
            {
                return StatusCode(500, "An error occurred while updated the league");
            }
            
        }


        [HttpDelete("DeleteChampionship")]
        public async Task<ActionResult> deleteChampionship(Championship championship)
        {
            bool isDeleted = await _championshipService.DeleteChampionshipAsync(championship);

            if(isDeleted)
            {
                return Ok();
            }
            else 
            {
                return NoContent();
            }

        }
    }
}
