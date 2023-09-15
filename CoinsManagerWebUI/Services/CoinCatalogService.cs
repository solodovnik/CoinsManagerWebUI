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
            var response = await _client.GetAsync("/api/continents");
            return await response.ReadContentAs<List<Continent>>();
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            var response = await _client.GetAsync($"/api/countries/{countryId}");
            return await response.ReadContentAs<Country>();
        }

        public async Task<Continent> GetContinentById(int continentId)
        {
            var response = await _client.GetAsync($"/api/continents/{continentId}");
            return await response.ReadContentAs<Continent>();
        }

        public async Task<IEnumerable<Country>> GetCountriesByContinentId(int continentId)
        {
            var response = await _client.GetAsync($"/api/continents/{continentId}/countries");
            return await response.ReadContentAs<List<Country>>();
        }

        public async Task<IEnumerable<Period>> GetPeriodsByCountryId(int countryId)
        {
            var response = await _client.GetAsync($"/api/countries/{countryId}/periods");
            return await response.ReadContentAs<List<Period>>();
        }

        public async Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId)
        {
            var response = await _client.GetAsync($"/api/periods/{periodId}/coins");
            return await response.ReadContentAs<List<Coin>>();
        }
        public async void CreateCoin(Coin coin)
        {
           

        }
    }
}
