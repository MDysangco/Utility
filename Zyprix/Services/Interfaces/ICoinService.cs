using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface ICoinService
    {
        public Task<List<Coin>> GetAllCoins();
        public Task<List<Coin>> GetActiveCoins();
        public Task<Coin> GetCoin(int coinId);
        public Task<bool> UpdateCoin(Coin coin, bool active, long binanceListingDate);

        //TODO:
        Coin CreateCoin(Coin coin);
        bool RemoveCoin(int coinId);
    }
}
