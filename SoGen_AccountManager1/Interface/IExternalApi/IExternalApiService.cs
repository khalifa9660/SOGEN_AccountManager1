using SoGen_AccountManager1.Models.ExternalApiModels;

namespace SoGen_AccountManager1.Interface.IExternalApi

{
	public interface IApiService
	{
        Task<Country[]> GetCountriesFromExternalApi();

        Task<Team[]> GetTeamsFromApi(int leagueId);

        Task<(TeamPlayer, Player[])> GetTeamAndPlayersFromExternalApi(int team);

        Task<(HistoryTeamMembers[], Coach[])> GetHistoryTeamMembersFromExternalApi(int season, int leagueId);

        }
}

