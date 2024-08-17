using System;

namespace SoGen_AccountManager1.Models.Domain
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? Number { get; set; }

        public string Position { get; set; }

        public string Nationality { get; set; }

        public string Photo { get; set; }

        public int UserId {get; set;}

        public int TeamId { get; set; }
    }
}

