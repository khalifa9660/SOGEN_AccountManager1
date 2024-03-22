using System;
using Microsoft.AspNetCore.Identity;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface
{
	public interface IPlayerRepository
	{
		Task<Player> AddPlayer(Player player);

		Task<IdentityUser> FindUserById(Guid userId);

		Task<IEnumerable<Player>> GetPlayersByUserId(Guid userId);

        Task<Player> EditPlayer(Player player);

		Task<bool> DeletePlayers(IEnumerable<Guid> ids);
    }
}

