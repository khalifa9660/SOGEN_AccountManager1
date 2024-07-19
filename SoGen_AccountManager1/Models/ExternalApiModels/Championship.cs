using System;

namespace SoGen_AccountManager1.Models.ExternalApiModels
{

    public class Championship
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DateTime Founded { get; set; }

        public string Photo { get; set; }

        public int Team_Id {get; set;}

    }
}





