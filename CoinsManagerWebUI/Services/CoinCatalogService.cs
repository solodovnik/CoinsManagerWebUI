using CoinsManagerWebUI.Extensions;
using CoinsManagerWebUI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Services
{
    public class CoinCatalogService : ICoinCatalogService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        public CoinCatalogService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        } 
        
        public async Task<IEnumerable<Continent>> GetAllContinents()
        {
            var accessToken = await GetToken();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await _client.GetAsync("/v1/Coins/GetAllContinents");
            return await response.ReadContentAs<List<Continent>>();
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            var response = await _client.GetAsync($"/v1/Coins/Countries/{countryId}");
            return await response.ReadContentAs<Country>();
        }

        public async Task<Continent> GetContinentById(int continentId)
        {
            var response = await _client.GetAsync($"/v1/Coins/Continents/{continentId}");
            return await response.ReadContentAs<Continent>();
        }

        public async Task<IEnumerable<Country>> GetCountriesByContinentId(int continentId)
        {
            var response = await _client.GetAsync($"/v1/Coins/GetCountriesByContinent?continentId={continentId}");
            return await response.ReadContentAs<List<Country>>();
        }

        public async Task<IEnumerable<Period>> GetPeriodsByCountryId(int countryId)
        {
            var response = await _client.GetAsync($"/v1/Coins/GetPeriodsByCountry?countryId={countryId}");
            return await response.ReadContentAs<List<Period>>();
        }

        public async Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId)
        {
            var response = await _client.GetAsync($"/v1/Coins/GetCoinsByPeriod?periodId={periodId}");
            return await response.ReadContentAs<List<Coin>>();
        }
        public async void CreateCoin(Coin coin)
        {
           

        }

        private async Task<string> GetToken()
        {
            var clientId = _configuration["Api:ClientId"];
            var clientSecret = _configuration["Api:ClientSecret"];
            var authority = _configuration["Api:Authority"];
            var applicationIdUri = _configuration["Api:ApplicationIdUri"];

            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(clientId)
           .WithClientSecret(clientSecret)
           .WithAuthority(authority)
           .Build();

            var authResult = await app.AcquireTokenForClient(new string[] { $"{applicationIdUri}/.default" }).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}
