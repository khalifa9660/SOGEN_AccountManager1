namespace SoGen_AccountManager1.Models.Domain
{
	public class AuthResult
	{
		public string Token { get; set; } = string.Empty;

		public bool Result { get; set; }

		public List<string> Errors { get; set; }


	}
}

