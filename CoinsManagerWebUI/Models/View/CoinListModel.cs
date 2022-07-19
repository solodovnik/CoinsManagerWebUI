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
            this.Periods = new List<Period>();
            this.Coins = new List<Coin>();
        }

        public List<Coin> Coins { get; set; }
        public List<SelectListItem> Continents { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<Period> Periods { get; set; }
        public int? ContinentId { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public Continent Continent { get; set; }
    }
}
