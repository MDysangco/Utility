using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface ICoinRepository
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
