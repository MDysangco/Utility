using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface IKlineRepository
    {
        public Kline GetLatestRecordedKline(Coin coin, KlineInterval interval);
        public Kline GetEarliestRecordedKline(Coin coin, KlineInterval interval);
        public bool InsertKlines(List<Kline> klines);
        public int DeleteKlinesByDateRange(long startDate, long endDate);

        //TODO:
        public IEnumerable<Kline> GetKlines();

    }
}
