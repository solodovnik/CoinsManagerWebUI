﻿using System.Collections.Generic;
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
        public async Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId)
        {
            var response = await _client.GetAsync($"/v1/Coin/GetCoinsByPeriod?periodId={periodId}");
            return await response.ReadContentAs<List<Coin>>();
        }

        public async Task<IEnumerable<Continent>> GetAllContinents()
        {            
            var response = await _client.GetAsync("/v1/Coin/GetAllContinents");
            return await response.ReadContentAs<List<Continent>>();
        }

        public async Task<IEnumerable<Country>> GetCountriesByContinentId(int continentId)
        {
            var response = await _client.GetAsync($"/v1/Coin/GetCountriesByContinent?continentId={continentId}");
            return await response.ReadContentAs<List<Country>>();
        }

        public async Task<IEnumerable<Period>> GetPeriodsByCountryId(int countryId)
        {
            var response = await _client.GetAsync($"/v1/Coin/GetPeriodsByCountry?countryId={countryId}");
            return await response.ReadContentAs<List<Period>>();
        }
    }
}
