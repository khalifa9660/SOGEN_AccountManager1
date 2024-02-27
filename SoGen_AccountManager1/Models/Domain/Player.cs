using System;

namespace SoGen_AccountManager1.Models.Domain
{
	public class Player
	{
		public int Id { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public int LastName { get; set; }

        public int Age { get; set; }

        public class Birth
        {
            public DateTime Date { get; set; }

            public string Place { get; set; }

            public string Country { get; set; }
        }

        public DateTime CreatedDate { get; set; }

        public string Nationality { get; set; }

        public string Weight { get; set; }

        public bool Injured { get; set; }

        public string Photo { get; set; }
    }
}

