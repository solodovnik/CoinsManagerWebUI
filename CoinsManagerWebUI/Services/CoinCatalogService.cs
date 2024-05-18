using CoinsManagerWebUI.Extensions;
using CoinsManagerWebUI.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Services
{
    public class CoinCatalogService : ICoinCatalogService
    {
        private readonly HttpClient _client;
        private readonly ILogger<CoinCatalogService> _logger;
        public CoinCatalogService(HttpClient client, ILogger<CoinCatalogService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<IEnumerable<Continent>> GetAllContinents()
        {
            return await HandleRequest<List<Continent>>(
                () => _client.GetAsync("/api/continents"),
                "Failed to get all continents");
        }

        public async Task<Country> GetCountryById(int countryId)
        {
            return await HandleRequest<Country>(
                () => _client.GetAsync($"/api/countries/{countryId}"),
                $"Failed to get country with ID {countryId}");
        }

        public async Task<Continent> GetContinentById(int continentId)
        {
            return await HandleRequest<Continent>(
                () => _client.GetAsync($"/api/continents/{continentId}"),
                $"Failed to get continent with ID {continentId}");
        }

        public async Task<IEnumerable<Country>> GetCountriesByContinentId(int continentId)
        {
            return await HandleRequest<List<Country>>(
                () => _client.GetAsync($"/api/continents/{continentId}/countries"),
                $"Failed to get countries for continent with ID {continentId}");
        }

        public async Task<IEnumerable<Period>> GetPeriodsByCountryId(int countryId)
        {
            return await HandleRequest<List<Period>>(
                () => _client.GetAsync($"/api/countries/{countryId}/periods"),
                $"Failed to get periods for country with ID {countryId}");
        }

        public async Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId)
        {
            return await HandleRequest<List<Coin>>(
                () => _client.GetAsync($"/api/periods/{periodId}/coins"),
                $"Failed to get coins for period with ID {periodId}");
        }

        public async Task<string> CreateCoin(Coin coin)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    nominal = coin.Nominal,
                    currency = coin.Currency,
                    year = coin.Year,
                    type = 1,
                    period = coin.Period,
                    pictPreviewPath = coin.PictPreviewPath
                }),
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync("/api/coins", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return "Coin added to collection successfully!";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to add new coin. Status code: {response.StatusCode}, Error: {errorContent}");
                throw new Exception("Failed to add new coin");
            }
        }

        private async Task<T> HandleRequest<T>(Func<Task<HttpResponseMessage>> requestFunc, string errorMessage)
        {
            try
            {
                var response = await requestFunc();

                if (response.IsSuccessStatusCode)
                {
                    return await response.ReadContentAs<T>();
                }
                else
                {
                    _logger.LogError($"{errorMessage}. Status code: {response.StatusCode}");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(errorContent))
                    {
                        _logger.LogError($"Error content: {errorContent}");
                    }
                    throw new Exception($"{errorMessage} {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"HTTP request failed: {ex.Message}");
                throw new Exception("HTTP request failed", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
