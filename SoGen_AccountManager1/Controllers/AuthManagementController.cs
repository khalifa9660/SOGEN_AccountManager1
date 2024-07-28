using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.Domain;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface.IAccountRepository;

namespace SoGen_AccountManager1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class AuthManagementController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private ILogger<AuthManagementController> _logger;
		private readonly IAccountRepository _accountRepository;
		private readonly JwtConfig _jwtConfig;



		public AuthManagementController(IConfiguration configuration, ILogger<AuthManagementController> logger, IAccountRepository accountRepository, IOptionsMonitor<JwtConfig> _optionsMonitor)
		{
			_configuration = configuration;
			_logger = logger;
			_jwtConfig = _optionsMonitor.CurrentValue;
			_accountRepository = accountRepository;
		}

		[HttpPost("Register")]

		public async Task<ActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
		{
			if (ModelState.IsValid)
			{
				//Check if Email exist
				var emailExist = await _accountRepository.FindUserByEmail(requestDto.Email);

				if(emailExist != null)
					return BadRequest(error: "Email already exist");

				var newUser = new User()
				{
					Mail = requestDto.Email,
					Name = requestDto.Name

				};

				var isCreated = await _accountRepository.CreateUserAsync(newUser, requestDto.Password);

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


		[HttpPost("Login")]

		public async Task<ActionResult> Login([FromBody] UserLoginRequestDto requestDto)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _accountRepository.FindUserByEmail(requestDto.Email);

				if (existingUser == null)
					return BadRequest("Invalid authentication");

				var isPasswordValid = await _accountRepository.CheckPasswordAsync(existingUser, requestDto.Password);

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


        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

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

		public async Task<bool> CreateUserAsync(User user, string password)
		{
			// Hacher le mot de passe
			user.Password = HashPassword(password);

			// Ajouter l'utilisateur à la base de données
			await _accountRepository.AddUser(user);

			return true;
		}

		public string HashPassword(string password)
		{
			// Utiliser une bibliothèque de hachage sécurisée, comme BCrypt
			return BCrypt.Net.BCrypt.HashPassword(password);
		}



    }
}

