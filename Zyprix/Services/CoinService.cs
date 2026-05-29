using Microsoft.Extensions.Caching.Memory;
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

        public async Task<List<Coin>> GetAllCoins() => await _coinRepository.GetAllCoins();
        public async Task<List<Coin>> GetActiveCoins() => await GetAllCoins();
        public async Task<Coin> GetCoin(int id) => await _coinRepository.GetCoin(id);
        public async Task<bool> UpdateCoin(Coin coin) => await _coinRepository.UpdateCoin(coin);

    }
}
