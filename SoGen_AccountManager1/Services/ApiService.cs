using System;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
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



        public async Task<Team[]> GetTeamsFromExternalApi(int leagueId)
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


        public async Task<Player[]> GetPlayersFromExternalApi(int season, int leagueId)
        {
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

                    // Extraire le tableau de pays (s'il est encapsulé dans un objet)
                    var PlayerArray = responseObject["api"]["players"].ToObject<Player[]>();
                    return PlayerArray;
                }


            }

        }
    }
}

