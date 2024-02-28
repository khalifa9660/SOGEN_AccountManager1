using System;
using System.Collections;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Models.Domain;

namespace SoGen_AccountManager1.Services
{
	public class ApiService : IApiService
	{
		public async Task<IEnumerable> GetCountriesFromExternalApi()
		{
			var client = new HttpClient();

			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/countries"),
				Headers =
				{
					{"X-RapidAPI-Key", "763f1903cemshf1814091940340dp16dfe1jsnb7781ae9d30a" },
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
					{ "x-ratelimit-requests-remaining", "95" },
					{ "x-ratelimit-requests-reset", "48525" },
					{ "x-request-id", "e3eb174d-cc61-4a4e-9ff5-e7aba19b2f0e" }

				}

			};

			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
				Console.WriteLine(body);
				return body;

			}
		}
	}
}

