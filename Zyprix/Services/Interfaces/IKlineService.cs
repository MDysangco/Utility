using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface IKlineService
    {
        public Task<Kline> GetLatestRecordedKline(int coinId, KlineInterval interval);
        public Task<Kline> GetEarliestRecordedKline(int coinId, KlineInterval interval);
        public Task<bool> InsertKlines(List<Kline> klines);
        public Task<int> DeleteKlinesByDateRange(long startDate, long endDate);

        //TODO:
        public IEnumerable<Kline> GetKlines();
    }
}
