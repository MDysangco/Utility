using Microsoft.Extensions.Caching.Memory;
using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class KlineService : IKlineService
    {
        private readonly IKlineRepository _klineRepository;
		private readonly IMemoryCache _memoryCache;
        private readonly ICoinService _coinService;

		public KlineService(IMemoryCache memoryCache, IKlineRepository klineRepository, ICoinService coinService) 
        {
			_memoryCache = memoryCache;
			_klineRepository = klineRepository;
			_coinService = coinService;
        }

        public async Task<List<Kline>> GetKlines(int? coinId, KlineInterval? interval)
        {
            string cacheKey = $"Klines_{coinId}_{interval}";

            if(_memoryCache.TryGetValue(cacheKey, out List<Kline> cachedKlines))
            {
                return cachedKlines;
            }

			Console.WriteLine($"Fetching klines from the database (CoinId: {coinId})...");
			List<Kline> klines = await _klineRepository.GetKlines(coinId, interval);

            //Should never expire, but we'll update it by adding and deleting klines as calling every row in every hour would be spenny.
			_memoryCache.Set(cacheKey, klines);

			return klines;
		}

        public async Task<Kline> GetLatestRecordedKline(int coinId, KlineInterval interval)
        {
            List<Kline> klines = await GetKlines(coinId, interval);
            return klines.OrderByDescending(k => k.KlineOpenTime).FirstOrDefault() ?? new Kline();
        }

		public async Task<Kline> GetEarliestRecordedKline(int coinId, KlineInterval interval)
		{
			List<Kline> klines = await GetKlines(coinId, interval);
			return klines.OrderBy(k => k.KlineOpenTime).FirstOrDefault() ?? new Kline();
		}

        public async Task<bool> InsertKlines(List<Kline> klines)
        {
            int? coinId = klines.FirstOrDefault()?.CoinId;
            KlineInterval? interval = klines.FirstOrDefault()?.Interval;

            if(coinId == null || interval == null)
            {
                return false;
            }

			string cacheKey = $"Klines_{coinId}_{interval}";

			List<Kline> cachedKlines = await GetKlines(coinId, interval);
            HashSet<long?> existingKlines = new HashSet<long?>(cachedKlines.Select(k => k.KlineOpenTime));
			List<Kline> newKlines = new List<Kline>();

			foreach (Kline kline in klines)
			{
				if (existingKlines.Add(kline.KlineOpenTime))
                {
                    newKlines.Add(kline);
				}
			}

            if (!newKlines.Any()) {
                return true;
			}

            bool result = await _klineRepository.InsertKlines(newKlines);
            if (!result) { 
                return false;
            }

			Console.WriteLine($"Inserted {newKlines.Count} klines (CoinId: {coinId})...");
			cachedKlines.AddRange(newKlines);
			cachedKlines = cachedKlines.OrderBy(k => k.KlineOpenTime).ToList();

			_memoryCache.Set(cacheKey, cachedKlines);

            return true;
        }

        public async Task<int> DeleteKlinesByDateRange(long startDate, long endDate)
        {
            List<Coin> coins = await _coinService.GetAllCoins();

			foreach (Coin coin in coins)
            {
                //Hard coded interval for now since we only user 1 hour klines.
                string cachedKey = $"Klines_{coin.Id}_{KlineInterval.OneHour}";

                if(_memoryCache.TryGetValue(cachedKey, out List<Kline>? cachedKlines))
                {
                    cachedKlines?.RemoveAll(k => 
                        k.KlineOpenTime >= startDate && 
                        k.KlineOpenTime < endDate
                    );

					_memoryCache.Set(cachedKey, cachedKlines);
				}

			}

			return await _klineRepository.DeleteKlinesByDateRange(startDate, endDate);
        }

    }
}
