using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class CoinService : ICoinService
    {
        private readonly ICoinRepository _coinRepository;

        public CoinService(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public IEnumerable<Coin> GetAllCoins() => _coinRepository.GetAllCoins();
        public IEnumerable<Coin> GetActiveCoins() => _coinRepository.GetActiveCoins();
        public Coin GetCoin(int id) => _coinRepository.GetCoin(id);
        public bool UpdateCoin(Coin coin, bool active, long listing) => _coinRepository.UpdateCoin(coin, active, listing);
        public Coin CreateCoin(Coin coin) => _coinRepository.CreateCoin(coin);
        public bool RemoveCoin(int id) => _coinRepository.RemoveCoin(id);
    }
}
