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

        [HttpGet("GetAllTeamsWithoutUserId")]

        public async Task<IActionResult> GetAllTeamsWithoutUserId()
        {
            try
            {
                var Teams = await _teamService.GetAllTeamsWithoutUserId();
                return Ok(Teams);
            }
            catch (Exception ex)
            {
                return NoContent();
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


        [HttpGet("GetTeamById/{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            var team = await _teamService.GetTeamById(teamId);

            if(team != null)
            {
                return Ok(team);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("EditTeam")]
        public async Task<IActionResult> EditTeam(TeamDTO teamDTO)
        {
            if(teamDTO == null){
                return BadRequest("Invalid Team data");
            }

            var existingTeam = await _teamService.GetTeamById(teamDTO.Id);

            if(existingTeam == null){
                return NotFound("Team with ID not found");
            }

            existingTeam.Name = teamDTO.Name;
            existingTeam.Photo = teamDTO.Photo;

            var editedTeam = await _teamService.EditTeamAsync(existingTeam);

            if(editedTeam != null){
                return Ok(editedTeam);
            }
            else{
                return StatusCode(500,"An error occurred while updating the team.");
            }
        }


        [HttpGet("GetTeamsByUserId/{userId}")]
        public async Task<ActionResult> GetTeamsByUserId(int userId)
        {
            var teams = await _teamService.GetTeamsByUserId(userId);

            if(teams != null)
            {
                return Ok(teams);
            }
            else 
            {
                return NoContent();
            }
        }


        [HttpGet("GetTeamsByChampionshipId/{championshipId}")]
        public async Task<ActionResult> GetTeamsByChampionshipId(int championshipId)
        {
            var teams = await _teamService.GetTeamsByChampionshipId(championshipId);

            if(teams != null)
            {
                return Ok(teams);
            }
            else
            {
                return NoContent();    
            }
        }


        [HttpDelete("DeleteTeam")]
        public async Task<ActionResult> deleteTeam(Team team)
        {
            bool isDeleted = await _teamService.deleteTeamAsync(team);

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
