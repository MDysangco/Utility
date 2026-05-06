using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class KlineService : IKlineService
    {
        private readonly IKlineRepository _klineRepository;

        public KlineService(IKlineRepository klineRepository) { 
            _klineRepository = klineRepository;
        }

        public Kline GetLatestRecordedKline(Coin coin, KlineInterval interval) => _klineRepository.GetLatestRecordedKline(coin, interval);
        public Kline GetEarliestRecordedKline(Coin coin, KlineInterval interval) => _klineRepository.GetEarliestRecordedKline(coin, interval);
        public bool InsertKlines(List<Kline> klines) => _klineRepository.InsertKlines(klines);
        public int DeleteKlinesByDateRange(long startDate, long endDate) => _klineRepository.DeleteKlinesByDateRange(startDate, endDate);
        public IEnumerable<Kline> GetKlines() => _klineRepository.GetKlines();

    }
}
