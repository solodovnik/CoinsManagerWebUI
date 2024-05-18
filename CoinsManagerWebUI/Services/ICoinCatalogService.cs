using CoinsManagerWebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Services
{
    public interface ICoinCatalogService
    {
        Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId);
        Task<IEnumerable<Continent>> GetAllContinents();
        Task<IEnumerable<Country>> GetCountriesByContinentId(int continentId);
        Task<Country> GetCountryById(int countryId);
        Task<Continent> GetContinentById(int continentId);
        Task<IEnumerable<Period>> GetPeriodsByCountryId(int countryId);
        Task<string> CreateCoin(Coin coin);
    }
}
