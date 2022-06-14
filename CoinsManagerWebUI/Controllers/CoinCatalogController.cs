using CoinsManagerWebUI.Models;
using CoinsManagerWebUI.Models.View;
using CoinsManagerWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        //public async Task<IActionResult> Index(int periodId)
        //{
        //    ViewBag.Title = "Coins";

        //    var getCoins = _coinCatalogService.GetCoinsByPeriod(periodId);
        //    await Task.WhenAll(new Task[] { getCoins });

        //    return View(new CoinListModel { Coins = getCoins.Result });
        //}

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Coins";

            //_viewModel = new CoinListModel();
            //var getCoins = _coinCatalogService.GetCoinsByPeriod(periodId);
            //await Task.WhenAll(new Task[] { getCoins });

            //return View(new CoinListModel { Coins = getCoins.Result });

            var continents = await _coinCatalogService.GetAllContinents();

            foreach (var continent in continents)
            {
                _viewModel.Continents.Add(new SelectListItem() { Value = continent.Id.ToString(), Text = continent.Continent1 });
            }

            ViewData["Continent"] = _viewModel.Continents;


            ViewData["Country"] = new List<SelectListItem>(); ;
            return View(_viewModel);
        }

        [HttpPost]
        public ActionResult Index(string SelectedPeriod)
        {
            var selectedContinent = Request.Form["Continent"].ToString();
            var selectedCountry = Request.Form["Country"].ToString();

            if (selectedContinent != null)
            {
                var getCountriesTask = _coinCatalogService.GetCountriesByContinentId(Convert.ToInt32(selectedContinent));
                foreach (var country in getCountriesTask.Result)
                    _viewModel.Countries.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Country1 });

                ViewData["Country"] = _viewModel.Countries;
            }

            if (selectedCountry != null)
            {
                var getPeriodsTask = _coinCatalogService.GetPeriodsByCountryId(Convert.ToInt32(selectedCountry));
                foreach (var period in getPeriodsTask.Result)
                    _viewModel.Periods.Add(new SelectListItem { Value = period.Id.ToString(), Text = period.Period1 });
            }

            if (SelectedPeriod != null)
            {
                var getCoinsTask = _coinCatalogService.GetCoinsByPeriod(Convert.ToInt32(SelectedPeriod));
                _viewModel.Coins.Clear();
                _viewModel.Coins.AddRange(getCoinsTask.Result);
            }

            return View(_viewModel);
        }

            //public IActionResult Add()
            //{
            //    ViewBag.Title = "Add Coin";
            //    return View(new Coin());
            //}

            //[HttpPost]
            //public async Task<IActionResult> Add(Coin model)
            //{
            //    if (ModelState.IsValid)
            //        await _coinCatalogService.Add(model);

            //    return RedirectToAction("Index");
            //}
        }
}
