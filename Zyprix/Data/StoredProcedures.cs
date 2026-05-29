namespace Zyprix.Data
{
    public class StoredProcedures
    {
        //Coins
        public const string GetAllCoins = "dbo.GetAllCoins";
        public const string GetActiveCoins = "dbo.GetActiveCoins";
        public const string GetCoin = "dbo.GetCoin";
        public const string UpdateCoin = "dbo.UpdateCoin";
        public const string UpdateCoins = "dbo.UpdateCoins";

        //Klines
        public const string DeleteKlinesByDateRange = "dbo.DeleteKlinesByDateRange";
        public const string GetEarliestRecordedKline = "dbo.GetEarliestRecordedKline";
        public const string GetLatestRecordedKline = "dbo.GetLatestRecordedKline";
        public const string InsertKlines = "dbo.InsertKlines";
        public const string GetKlines = "dbo.GetKlines";

		//Reading
        public const string GetReadings = "dbo.GetReadings";
		public const string InsertReading = "dbo.InsertReading";
		public const string InsertReadings = "dbo.InsertReadings";

		//Configuration
		public const string InsertConfiguration = "dbo.InsertConfiguration";
		public const string InsertConfigurations = "dbo.InsertConfigurations";
	}
}
