using CoinsManagerWebUI.Models.View;
using CoinsManagerWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Controllers
{
    public class CoinCatalogController : Controller
    {
        private readonly ICoinCatalogService _coinCatalogService;
        private CoinListModel _viewModel = new CoinListModel();

        public CoinCatalogController(ICoinCatalogService coinCatalogService)
        {
            _coinCatalogService = coinCatalogService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Coins";

            var continents = await _coinCatalogService.GetAllContinents();

            foreach (var continent in continents)
            {
                _viewModel.Continents.Add(new SelectListItem() { Value = continent.Id.ToString(), Text = continent.Continent1 });
            }

            ViewData["Continent"] = _viewModel.Continents;
            return View(_viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SelectCountry(CoinListModel model)
        {
            var selectedContinent = model.ContinentId;
            var countries = await _coinCatalogService.GetCountriesByContinentId(Convert.ToInt32(selectedContinent));
            foreach (var country in countries)
            {
                _viewModel.Countries.Add(new SelectListItem() { Value = country.Id.ToString(), Text = country.Country1 });
            }
            return View(_viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Coin(int countryId)
        {
            var country = await _coinCatalogService.GetCountryById(countryId);
            _viewModel.Country = country;
            var continent = await _coinCatalogService.GetContinentById(country.Continent);
            _viewModel.Continent = continent;
            var periods = await _coinCatalogService.GetPeriodsByCountryId(countryId);
            _viewModel.Periods = periods.ToList();
            foreach (var period in periods)
            {
                var coins = await _coinCatalogService.GetCoinsByPeriod(Convert.ToInt32(period.Id));
                _viewModel.Coins.AddRange(coins);
            }
            return View("Coins", _viewModel);
        }
    }
}
