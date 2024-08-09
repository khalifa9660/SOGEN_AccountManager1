using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
