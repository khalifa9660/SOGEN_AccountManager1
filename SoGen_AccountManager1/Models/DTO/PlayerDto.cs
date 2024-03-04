using System;
namespace SoGen_AccountManager1.Models.DTO
{
	public class PlayerDto
	{
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string LastName { get; set; }

        public int Age { get; set; }

        public DateTime Birth { get; set; }

        public int Weight { get; set; }

        public string? Photo { get; set; }
    }
}

