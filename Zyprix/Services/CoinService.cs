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
        private readonly IMemoryCache _memoryCache;

        public CoinService(IMemoryCache memoryCache, ICoinRepository coinRepository)
        {
            _memoryCache = memoryCache;
            _coinRepository = coinRepository;
        }

        public async Task<List<Coin>> GetAllCoins()
        {
			const string cacheKey = "all_coins";

			if (_memoryCache.TryGetValue(cacheKey, out List<Coin> cachedCoins))
			{
				return cachedCoins;
			}

            Console.WriteLine("Fetching all coins from the database...");
			List<Coin> coins = await _coinRepository.GetAllCoins();

			//Cached for 1 day, as the active coins list is not expected to change frequently. This will reduce database load and improve performance.
			_memoryCache.Set(cacheKey, coins, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)});

            return coins;
        }

        public async Task<List<Coin>> GetActiveCoins()
        {
            const string cacheKey = "active_coins";

			if (_memoryCache.TryGetValue(cacheKey, out List<Coin> cachedCoins))
			{
				return cachedCoins;
			}

			List<Coin> allCoins = await GetAllCoins();
			List<Coin> activeCoins = allCoins.Where(c => c.Active == true).ToList();

			//Cached for 1 day, as the active coins list is not expected to change frequently. This will reduce database load and improve performance.
			_memoryCache.Set(cacheKey, activeCoins, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)});

            return activeCoins;
        }

        public async Task<Coin> GetCoin(int id)
        {
            List<Coin> coins = await GetAllCoins();
			return coins.FirstOrDefault(c => c.Id == id) ?? new Coin();
		}

        public async Task<bool> UpdateCoin(Coin coin, bool active, long listing)
        {
            bool updated = await _coinRepository.UpdateCoin(coin, active, listing);
            if (updated)
            {
				Console.WriteLine("Removing coins from cache...");
				_memoryCache.Remove("all_coins");
				_memoryCache.Remove("active_coins");
			}

			return updated;
        }

        //TODO:
        public Coin CreateCoin(Coin coin) => _coinRepository.CreateCoin(coin);
        public bool RemoveCoin(int id) => _coinRepository.RemoveCoin(id);
    }
}
