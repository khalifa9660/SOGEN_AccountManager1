using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.DTO;
using SoGen_AccountManager1.Repositories.Interface.IService;

namespace SoGen_AccountManager1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class AuthManagementController : ControllerBase
	{
		private readonly IUserService _userService;

		public AuthManagementController(IUserService userService)
		{
			_userService = userService;
		}


		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid request payload");
				}

				var emailExist = await _userService.FindByEmailAsync(userRegisterDTO.Email);

				if (emailExist != null)
				{
					return BadRequest("Email already exists");
				}

				var (user, token) = await _userService.RegisterUserAsync(userRegisterDTO);

				if (user != null)
				{
					return Ok(new { User = user, Token = token });
				}
				else
				{
					return BadRequest("User registration failed");
				}
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}



		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest("Invalid request payload");
				}

				var (user, token) = await _userService.LoginAsync(userLoginDTO.Email, userLoginDTO.Password);

				if (user != null)
				{
					var userInfo = new
					{
						Email = user.Email,
						Token = token
					};

					return Ok(userInfo);
				}
				else
				{
					return Unauthorized("Invalid email or password");
				}
			}
			catch (ArgumentException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (UnauthorizedAccessException)
			{
				return Unauthorized("Invalid email or password");
			}
			catch (Exception ex)
			{
				// Loggez l'exception ici
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}
    }
}

