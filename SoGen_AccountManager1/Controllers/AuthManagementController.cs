using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Models.DTO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SoGen_AccountManager1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class AuthManagementController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private ILogger<AuthManagementController> _logger;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly JwtConfig _jwtConfig;

		public AuthManagementController(IConfiguration configuration, ILogger<AuthManagementController> logger, UserManager<IdentityUser> userManager, IOptionsMonitor<JwtConfig> _optionsMonitor)
		{
			_configuration = configuration;
			_logger = logger;
			_jwtConfig = _optionsMonitor.CurrentValue;
			_userManager = userManager;
		}

		[HttpPost("Register")]

			public async Task<ActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
		{
			if (ModelState.IsValid)
			{
				//Check if Email exist
				var emailExist = await _userManager.FindByEmailAsync(requestDto.Email);

				if(emailExist != null)
					return BadRequest(error: "Email already exist");

				var newUser = new IdentityUser()
				{
					Email = requestDto.Email,
					UserName = requestDto.Name

				};

				var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

				if (isCreated.Succeeded)
				{
					//Generate Token

					var token = GenerateJwtToken(newUser);

					return Ok(new RegistrationRequestResponse()
					{
						Result = true,
						Token = token
					});
				}

				return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
				
			}

			return BadRequest(error: "Invalid request payload");
		}


        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Générer une clé aléatoire de 512 bits
            //byte[] keyBytes = new byte[64]; // 512 bits / 8 = 64 octets
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(keyBytes);
            //}

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                            SecurityAlgorithms.HmacSha512)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }



		[HttpPost("Login")]

		public async Task<ActionResult> Login([FromBody] UserLoginRequestDto requestDto)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);

				if (existingUser == null)
					return BadRequest("Invalid authentication");

				var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, requestDto.Password);

				if (isPasswordValid)
				{
					var token = GenerateJwtToken(existingUser);

					return Ok(new LoginRequestResponse()
					{
                        Token = token,
						Result = true
					});
				}

                return BadRequest("Invalid authentication");

            }

            return BadRequest(error: "Invalid request payload");
        }

	}
}

