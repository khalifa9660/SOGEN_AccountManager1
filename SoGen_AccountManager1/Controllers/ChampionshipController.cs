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
        public async Task<IActionResult> AddChampionship(ChampionshipDTO championshipDTO)
        {
            try
            {
                var championship = await _championshipService.AddChampionshipAsync(championshipDTO);
                return Ok(championshipDTO);
            } 
            catch
            {
                return NoContent();
            }
        }

        [HttpGet("GetAllChampionships")]
        
        public async Task<IActionResult> GetAllChampionship()
        {
             try
             {
                var allChampionship = await _championshipService.GetAllChampionships();
                return Ok(allChampionship);
             }
             catch
             {
                return NoContent();
             }
        }

        [HttpGet("GetChampionshipById")]
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

        [HttpPost("EditChampionship")]
        public async Task<IActionResult> EditChampionship(ChampionshipDTO championshipDTO)
        {
            var championship = new Championship
        {
            Name = championshipDTO.Name,
            Country = championshipDTO.Country
        };

        var editedChampionship = await _championshipService.EditChampionship(championship);

        if(editedChampionship != null)
        {
            return Ok(editedChampionship);
        } 
        else
        {
            return NoContent();
        }
            
        }


        [HttpGet("GetChampionshipByUserId")]
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
