using System;
namespace SoGen_AccountManager1.Models.Domain
{
	public class Team
	{
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public string? Country { get; set; }
    }
}

