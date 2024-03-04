using System;

namespace SoGen_AccountManager1.Models.Domain
{
	public class Player
	{
		public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public DateTime Birth { get; set; }

        public string? Place { get; set; }

        public string? Country { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Nationality { get; set; }

        public int Weight { get; set; }

        public bool? Injured { get; set; }

        public string? Photo { get; set; }
    }
}

