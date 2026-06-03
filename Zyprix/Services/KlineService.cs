using Microsoft.Extensions.Caching.Memory;
using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class KlineService : IKlineService
    {
        private readonly IKlineRepository _klineRepository;

		public KlineService(IKlineRepository klineRepository) 
        {
			_klineRepository = klineRepository;
        }

        public async Task<List<Kline>> GetKlines(int? coinId, KlineInterval? interval) => await _klineRepository.GetKlines(coinId, interval);
        public async Task<Kline> GetLatestRecordedKline(int coinId, KlineInterval interval) => await _klineRepository.GetLatestRecordedKline(coinId, interval);
		public async Task<Kline> GetEarliestRecordedKline(int coinId, KlineInterval interval) => await _klineRepository.GetEarliestRecordedKline(coinId, interval);
        public async Task<bool> InsertKlines(List<Kline> klines) => await _klineRepository.InsertKlines(klines);

    }
}
