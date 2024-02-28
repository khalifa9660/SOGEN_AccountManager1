using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Models.Domain;

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
    }

}

