using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface ICoinService
    {
        IEnumerable<Coin> GetAllCoins();
        IEnumerable<Coin> GetActiveCoins();
        Coin GetCoin(int coinId);
        bool UpdateCoin(Coin coin, bool active, long binanceListingDate);

        //TODO:
        Coin CreateCoin(Coin coin);
        bool RemoveCoin(int coinId);
    }
}
