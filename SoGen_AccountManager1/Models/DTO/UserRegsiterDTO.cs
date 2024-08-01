using System.ComponentModel.DataAnnotations;

namespace SoGen_AccountManager1.Models.DTO
{
	public class UserRegisterDTO
	{

		[Required]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		public string LastName { get; set; } = string.Empty;

		[Required]
        [EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
        [MinLength(8)]
		public string Password { get; set; } = string.Empty;
	}
}

