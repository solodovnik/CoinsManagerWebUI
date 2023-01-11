using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoinsManagerWebUI.Extensions;
using CoinsManagerWebUI.Models;

namespace CoinsManagerWebUI.Services
{
    public class CoinCatalogService : ICoinCatalogService
    {
        private readonly HttpClient _client;

        public CoinCatalogService(HttpClient client)
        {
            _client = client;
        }       

        public async Task<IEnumerable<Continent>> GetAllContinents()
        {            
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
    }
}
