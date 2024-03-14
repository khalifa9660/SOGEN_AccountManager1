﻿using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoGen_AccountManager1.Models.ApiModels;

namespace SoGen_AccountManager1.Interface

{
	public interface IApiService
	{
        Task<Country[]> GetCountriesFromExternalApi();

        Task<Team[]> GetTeamsFromApi(int leagueId);

        Task<(TeamPlayer, Player[])> GetTeamAndPlayersFromExternalApi(int team);

        Task<(HistoryTeamMembers[], Coach[])> GetHistoryTeamMembersFromExternalApi(int season, int leagueId);

        }
}

