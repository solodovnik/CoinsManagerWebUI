using CoinsManagerWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Services
{
    public interface ICoinCatalogService
    {
        Task<IEnumerable<Coin>> GetCoinsByPeriod(int periodId);
    }
}
