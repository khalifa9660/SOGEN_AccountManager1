﻿using System;
namespace SoGen_AccountManager1.Models.Domain
{
	public class User
	{
		public int Id { get; set; }

		public string Username { get; set; } = string.Empty;

		public string PasswordHash { get; set; } = string.Empty;


	}
}

