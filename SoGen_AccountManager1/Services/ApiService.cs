using Newtonsoft.Json.Linq;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Models.ApiModels;

namespace SoGen_AccountManager1.Services
{
    public class ApiService : IApiService
    {
        public async Task<Country[]> GetCountriesFromExternalApi()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/countries"),
                Headers =
        {
            { "X-RapidAPI-Key", "763f1903cemshf1814091940340dp16dfe1jsnb7781ae9d30a" },
            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
            { "access-control-allow-credentials", "true" },
            { "access-control-allow-headers", "x-rapidapi-key, x-apisports-key, x-rapidapi-host" },
            { "access-control-allow-methods", "GET, OPTIONS" },
            { "access-control-allow-origin", "*" },
            { "server", "RapidAPI-1.2.8" },
            { "vary", "Accept-Encoding" },
            { "x-rapidapi-region", "AWS - eu-central-1" },
            { "x-rapidapi-version", "1.2.8" },
            { "x-ratelimit-requests-limit", "100" },

        }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                // Désérialiser le corps de la réponse en tant qu'objet JObject
                var responseObject = JObject.Parse(body);

                // Extraire le tableau de pays (s'il est encapsulé dans un objet)
                var countriesArray = responseObject["response"].ToObject<Country[]>();
                return countriesArray;
            }
        }



        public async Task<Team[]> GetTeamsFromApi(int leagueId)
        {
            var client = new HttpClient();

            string apiUrl = $"https://api-football-v1.p.rapidapi.com/v2/teams/league/{leagueId}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
                {
                    { "X-RapidAPI-Key", "763f1903cemshf1814091940340dp16dfe1jsnb7781ae9d30a" },
                    { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    { "access-control-allow-credentials", "true" },
                    { "access-control-allow-headers", "x-rapidapi-key, x-apisports-key, x-rapidapi-host" },
                    { "access-control-allow-methods", "GET, OPTIONS" },
                    { "access-control-allow-origin", "*" },
                    { "server", "RapidAPI-1.2.8" },
                    { "vary", "Accept-Encoding" },
                    { "x-rapidapi-region", "AWS - eu-central-1" },
                    { "x-rapidapi-version", "1.2.8" },
                    { "x-ratelimit-requests-limit", "100" }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                // Désérialiser le corps de la réponse en tant qu'objet JObject
                var responseObject = JObject.Parse(body);

                // Extraire le tableau de pays (s'il est encapsulé dans un objet)
                var TeamsArray = responseObject["api"]["teams"].ToObject<Team[]>();
                return TeamsArray;
            }


        }


        public async Task<(TeamPlayer, Player[])> GetTeamAndPlayersFromExternalApi(int team)
        {
            var client = new HttpClient();

            string apiUrl = $"https://api-football-v1.p.rapidapi.com/v3/players/squads?team={team}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
        {
            { "X-RapidAPI-Key", "763f1903cemshf1814091940340dp16dfe1jsnb7781ae9d30a" },
            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
            { "access-control-allow-credentials", "true" },
            { "access-control-allow-headers", "x-rapidapi-key, x-apisports-key, x-rapidapi-host" },
            { "access-control-allow-methods", "GET, OPTIONS" },
            { "access-control-allow-origin", "*" },
            { "server", "RapidAPI-1.2.8" },
            { "vary", "Accept-Encoding" },
            { "x-rapidapi-region", "AWS - eu-central-1" },
            { "x-rapidapi-version", "1.2.8" },
            { "x-ratelimit-requests-limit", "100" },
            { "nel", "{\"report_to\":\"heroku-nel\",\"max_age\":3600,\"success_fraction\":0.005,\"failure_fraction\":0.05,\"response_headers\":[\"Via\"]}" },
            { "report-to", "{\"group\":\"heroku-nel\",\"max_age\":3600,\"endpoints\":[{\"url\":\"https://nel.heroku.com/reports?ts=1710253011&sid=c4c9725f-1ab0-44d8-820f-430df2718e11&s=G7iN08eGxPJVMWoqg1Jxz3%2F1JfpBAq7yPr8GShluCk4%3D\"}]}" },
            { "reporting-endpoints", "heroku-nel=https://nel.heroku.com/reports?ts=1710253011&sid=c4c9725f-1ab0-44d8-820f-430df2718e11&s=G7iN08eGxPJVMWoqg1Jxz3%2F1JfpBAq7yPr8GShluCk4%3D" },
            { "via", "1.1 vegur"}
        }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                // Désérialiser le corps de la réponse en tant qu'objet JObject
                var responseObject = JObject.Parse(body);

                // Désérialiser les informations de l'équipe
               var teamInfo = responseObject["response"][0]["team"].ToObject<TeamPlayer>();

                // Désérialiser le tableau de joueurs
                var playersArray = responseObject["response"][0]["players"].ToObject<Player[]>();

                return (teamInfo, playersArray);
            }
        }


        public async Task<(HistoryTeamMembers[], Coach[])> GetHistoryTeamMembersFromExternalApi(int season, int leagueId)
        {
            var client = new HttpClient();

            string apiUrl = $"https://api-football-v1.p.rapidapi.com/players/{season}/{leagueId}";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers =
        {
            { "X-RapidAPI-Key", "763f1903cemshf1814091940340dp16dfe1jsnb7781ae9d30a" },
            { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
            { "access-control-allow-credentials", "true" },
            { "access-control-allow-headers", "x-rapidapi-key, x-apisports-key, x-rapidapi-host" },
            { "access-control-allow-methods", "GET, OPTIONS" },
            { "access-control-allow-origin", "*" },
            { "server", "RapidAPI-1.2.8" },
            { "vary", "Accept-Encoding" },
            { "x-rapidapi-region", "AWS - eu-central-1" },
            { "x-rapidapi-version", "1.2.8" },
            { "x-ratelimit-requests-limit", "100" }
        }
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                // Désérialiser le corps de la réponse en tant qu'objet JObject
                var responseObject = JObject.Parse(body);

                // Extraire le tableau de joueurs
                var playerArray = responseObject["api"]["players"].ToObject<HistoryTeamMembers[]>();

                // Extraire le tableau de coachs et les transformer en objets Coach
                var coachsArray = responseObject["api"]["coachs"].Select(coachName => new Coach { Name = (string)coachName }).ToArray();

                return (playerArray, coachsArray);
            }
        }
    }

}