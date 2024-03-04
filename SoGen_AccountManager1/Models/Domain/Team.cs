using System;
namespace SoGen_AccountManager1.Models.Domain
{
	public class Team
	{
        public int Team_id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public string? Country { get; set; }

        public bool Is_national { get; set; }

        public int Founded { get; set; }

        public string? Venue_name { get; set; }

        public string? Venue_surface { get; set; }

        public string? Venue_city { get; set; }

        public string? Venue_adress { get; set; }

        public int Venue_capacity { get; set; }


    }
}

