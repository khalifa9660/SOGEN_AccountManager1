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
        private readonly ITeamService _teamService;
        private readonly IPlayerService _playerService;

        public ChampionshipController(IChampionshipService championshipService, ITeamService teamService, IPlayerService playerService)
        {
            _championshipService = championshipService;
            _teamService = teamService;
            _playerService = playerService;
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
        

        [HttpPut("EditChampionship")]
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
            existingLeague.Photo = championshipDTO.Photo;

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


        [HttpDelete("DeleteChampionship/{id}")]
        public async Task<ActionResult> deleteChampionship(int id)
        {
            var checkIfTeamExistsAsync = await _teamService.GetTeamsByChampionshipId(id);

            if(checkIfTeamExistsAsync != null && checkIfTeamExistsAsync.Any())
            {
                foreach(var team in checkIfTeamExistsAsync)
                {
                    var playersToDelete = await _playerService.GetPlayersByTeamId(team.Id);

                    if(playersToDelete != null && playersToDelete.Any())
                    {
                        var playerIds = playersToDelete.Select(player => player.Id).ToList();
                        await _playerService.DeletePlayersAsync(playerIds);
                    }

                    await _teamService.deleteTeamAsync(team.Id);
                }
            }

            bool isDeleted = await _championshipService.DeleteChampionshipAsync(id);

            if(isDeleted)
            {
                return Ok();
            }
            else 
            {
                return StatusCode(500, "An error occurred while deleting the league");
            }

        }
    }
}
