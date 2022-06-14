using System;
using System.Collections.Generic;
using System.Linq;
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
            //client.BaseAddress = new Uri(Baseurl);
            //client.DefaultRequestHeaders.Clear();
            ////Define request data format
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ////Sending request to find web api REST service resource GetAllEmployees using HttpClient
            //HttpResponseMessage Res = await client.GetAsync("api/Employee/GetAllEmployees");
            ////Checking the response is successful or not which is sent using HttpClient
            //if (Res.IsSuccessStatusCode)
            //{
            //    //Storing the response details recieved from web api
            //    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
            //    //Deserializing the response recieved from web api and storing into the Employee list
            //    EmpInfo = JsonConvert.DeserializeObject<List<Employee>>(EmpResponse);
            //}
            //var response = await _client.GetAsync("/v1/Coin?periodId=1");
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
            var response = await _client.GetAsync($"/v1/Coin/GetPeriodsByCountry?continentId={countryId}");
            return await response.ReadContentAs<List<Period>>();
        }
    }
}
