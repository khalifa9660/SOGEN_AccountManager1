using System;
namespace SoGen_AccountManager1.Models.DTO
{
	public class UserDto
	{
		public int Id { get; set; }

		public required string Username { get; set; }

		public required string Password { get; set; }

	}
}

