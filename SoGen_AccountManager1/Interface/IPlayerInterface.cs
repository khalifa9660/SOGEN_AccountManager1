﻿using System;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Repositories.Interface
{
	public interface IPlayerRepository
	{
		Task<Player> AddPlayer(Player player);

		Task<IEnumerable<Player>> GetPlayers();

		Task<Player> EditPlayer(Player player);

		Task<Player> GetPlayerToEdit(Guid id);

		Task<bool> DeletePlayer(Player player);
    }
}

