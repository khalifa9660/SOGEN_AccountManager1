using System;
namespace SoGen_AccountManager1.Models.ExternalApiModels
{
	public class Team
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Logo { get; set; }

        public string Country { get; set; }

        public bool Is_national { get; set; }

        public int Founded { get; set; }

        public string Venue_name { get; set; }

        public string Venue_surface { get; set; }

        public string Venue_city { get; set; }

        public string Venue_address { get; set; }

        public int Venue_capacity { get; set; }
    }
}

