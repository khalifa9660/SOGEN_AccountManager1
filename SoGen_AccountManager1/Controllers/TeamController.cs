using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost("AddTeam")]
        public async Task<IActionResult> AddTeam([FromBody]TeamDTO teamDTO)
        {
            try
            {
                var team = await _teamService.AddTeamAsync(teamDTO);
                return Ok(team);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllTeams")]

        public async Task<IActionResult> GetAllTeams()
        {
            try
            {
                var Teams = await _teamService.GetAllTeams();
                return Ok(Teams);
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }


        [HttpGet("GetTeamById")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            var team = await _teamService.GetTeamByid(teamId);

            if(team != null)
            {
                return Ok(team);
            }
            else
            {
                return NoContent();
            }
        }

    }
}
