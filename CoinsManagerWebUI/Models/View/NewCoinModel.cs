using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CoinsManagerWebUI.Models.View
{
    public class NewCoinModel
    {
        public NewCoinModel()
        {
            this.Continents = new List<SelectListItem>();
            this.Countries = new List<SelectListItem>();
            this.Periods = new List<Period>();
            this.CoinTypes = new List<CoinType>();
        }
       
        public List<SelectListItem> Continents { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<Period> Periods { get; set; }
        public List<CoinType> CoinTypes { get; set; }
        public int? ContinentId { get; set; }
        public int? CountryId { get; set; }
        public int? PeriodId { get; set; }
        public Country Country { get; set; }
        public Continent Continent { get; set; }
        public Coin Coin { get; set; }
    }
}
