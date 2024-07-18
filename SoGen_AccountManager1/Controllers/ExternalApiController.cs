using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Interface;
 
namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class FootBallApiController : ControllerBase
    {
        private readonly IApiService _apiService;

        public FootBallApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> GetCountriesFromApi()
        {
            try
            {
                var countries = await _apiService.GetCountriesFromExternalApi();

                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des pays : {ex.Message}");
            }
        }


        [HttpGet("Teams/{leagueId}")]
        public async Task<IActionResult> GetTeamsFromApi(int leagueId)
        {
            try
            {
                var Teams = await _apiService.GetTeamsFromApi(leagueId);

                return Ok(Teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des équipes : {ex.Message}");
            }
        }


        [HttpGet("Players/{team}")]
        public async Task<ActionResult> GetPlayersFromApi(int team)
        {
            try
            {
                var (teamInfo, players) = await _apiService.GetTeamAndPlayersFromExternalApi(team);

                var responseData = new
                {
                    Team = teamInfo,
                    Players = players
                };

                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des équipes : {ex.Message}");
            }
        }

        [HttpGet("HistoryTeamMembers/{season}/{leagueId}")]
        public async Task<ActionResult> GetHistoryTeamsFromApi(int season, int leagueId)
        {

            try
            {
                var (players, coach) = await _apiService.GetHistoryTeamMembersFromExternalApi(season, leagueId);

                var responseData = new
                {
                    Players = players,
                    Coachs = coach
                };

                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des équipes : {ex.Message}");
            }
        }
    }
}

