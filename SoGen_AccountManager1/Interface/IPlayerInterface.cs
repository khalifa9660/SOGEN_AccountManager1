using System;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface
{
	public interface IPlayerRepository
	{
		Task<Player> CreateAsync(Player player);
	}
}

