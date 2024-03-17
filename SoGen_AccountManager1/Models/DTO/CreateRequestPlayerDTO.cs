using System;
namespace SoGen_AccountManager1.Models.DTO
{
	public class UpdatePlayerDTO
	{
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? Number { get; set; }

        public string Position { get; set; }

        public string Photo { get; set; }
    }
}

