using CoinsManagerWebUI.Models;
using CoinsManagerWebUI.Models.View;
using CoinsManagerWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CoinsManagerWebUI.Controllers
{
    public class NewCoinController : Controller
    {
        private readonly ICoinCatalogService _coinCatalogService;
        private readonly ILogger<NewCoinController> _logger;

        public NewCoinController(ICoinCatalogService coinCatalogService, ILogger<NewCoinController> logger)
        {
            _coinCatalogService = coinCatalogService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new NewCoinModel();
            var continents = await _coinCatalogService.GetAllContinents();
            
            foreach (var continent in continents)
            {
                viewModel.Continents.Add(new SelectListItem() { Value = continent.Id.ToString(), Text = continent.Continent1 });
            }

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"];
            }

            return View(viewModel);
        }

        public async Task<IActionResult> CreateCoin(NewCoinModel model)
        {
            var responseMessage = await _coinCatalogService.CreateCoin(model.Coin);
            TempData["Message"] = responseMessage;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCountries(string id)
        {
            if (id != null)
            {
                var countries = (await _coinCatalogService.GetCountriesByContinentId(int.Parse(id))).ToList();
                return Json(new SelectList(countries, "Id", "Country1"));
            }
            else
                return Json(string.Empty);
        }

        [HttpPost]
        public async Task<IActionResult> GetPeriods(string id)
        {
            var periods = (await _coinCatalogService.GetPeriodsByCountryId(int.Parse(id))).ToList();
            return Json(new SelectList(periods, "Id", "Period1"));
        }
    }
}
