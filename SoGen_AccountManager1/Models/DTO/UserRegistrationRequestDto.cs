using System;
using System.ComponentModel.DataAnnotations;

namespace SoGen_AccountManager1.Models.DTO
{
	public class UserRegistrationRequestDto
	{
		[Required]
		public string Name { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
	}
}

