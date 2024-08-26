namespace SoGen_AccountManager1.Models.Domain
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public DateTime Founded {get; set;}

        public int? UserId {get; set;}

        public int? ChampionshipId {get; set;}

    }
}