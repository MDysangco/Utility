using System;
using System.Collections.Generic;
using System.Text;

namespace Zyprix.Data
{
    public class StoredProcedures
    {
        //Coins
        public const string GetAllCoins = "dbo.GetAllCoins";
        public const string GetActiveCoins = "dbo.GetActiveCoins";
        public const string GetCoin = "dbo.GetCoin";
        public const string RemoveCoin = "dbo.RemoveCoin";
        public const string UpdateCoin = "dbo.UpdateCoin";

        //Klines
        public const string DeleteKlinesByDateRange = "dbo.DeleteKlinesByDateRange";
        public const string GetEarliestRecordedKline = "dbo.GetEarliestRecordedKline";
        public const string GetLatestRecordedKline = "dbo.GetLatestRecordedKline";
        public const string InsertKlines = "dbo.InsertKlines";
    }
}
