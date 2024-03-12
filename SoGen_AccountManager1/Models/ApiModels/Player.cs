using System;
using Newtonsoft.Json;

namespace SoGen_AccountManager1.Models.ApiModels
{

    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? Number { get; set; }

        public string Position { get; set; }

        public string Photo { get; set; }


    }

    public class TeamPlayer
    {
        public int id { get; set; }

        public string name { get; set; }

        public string logo { get; set; }
    }

    public class NationalPlayer
    {
        public string Number { get; set; }

        public string player { get; set; }
    }



}





