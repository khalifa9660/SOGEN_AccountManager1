using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Models.ApiModels;

namespace SoGen_AccountManager1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExternalApiController : ControllerBase
    {
        private readonly IApiService _apiService;

        public ExternalApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet(Name = "Countries")]
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


        [HttpGet("{leagueId}")]
        public async Task<IActionResult> GetTeamsFromApi(int leagueId)
        {
            try
            {

                var Teams = await _apiService.GetTeamsFromExternalApi(leagueId);

                return Ok(Teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des équipes : {ex.Message}");
            }
        }


        [HttpGet("{season}/{leagueId}")]
        public async Task<ActionResult> GetPlayersFromApi(int season, int leagueId)
        {

            try
            {
                var players = await _apiService.GetPlayersFromExternalApi(season, leagueId);
                return Ok(players);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite lors de la récupération des équipes : {ex.Message}");
            }
        }

        //To add an authorisation to get the data
        //[HttpGet("{season}/{leagueId}")]
        //[Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]


    }
}

