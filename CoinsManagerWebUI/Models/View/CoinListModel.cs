using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Models.View
{
    public class CoinListModel
    {
        public IEnumerable<Coin> Coins { get; set; }
    }
}
