using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoinsManagerWebUI.Models.View
{
    public class CoinListModel
    {
        public CoinListModel()
        {
            this.Continents = new List<SelectListItem>();
            this.Countries = new List<SelectListItem>();
            this.Periods = new List<SelectListItem>();
            this.Coins = new List<Coin>();
        }

        public List<Coin> Coins { get; set; }
        public List<SelectListItem> Continents { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> Periods { get; set; }
        public string SelectedContinent { get; set; }
        public string SelectedCountry { get; set; }
        public string SelectedPeriod { get; set; }
    }
}
