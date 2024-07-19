namespace SoGen_AccountManager1.Models.Domain
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

        public int Championship_Id {get; set;}

        public DateTime Founded {get; set;}

        public int User_id { get; set; }
    }
}