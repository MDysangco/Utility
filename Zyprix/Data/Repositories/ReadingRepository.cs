using Microsoft.Data.SqlClient;
using System.Data;
using Utils;
using Zyprix.Data.Interfaces;
using Zyprix.Models;

namespace Zyprix.Data.Repositories
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly string _connectionString;

        public ReadingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Reading>> GetReadings(int coinId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetReadings, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@CoinId", SqlDbType.Int).Value = coinId;

                    await conn.OpenAsync();

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
                        List<Reading> readings = new List<Reading>();

						while (await reader.ReadAsync())
						{
							readings.Add(reader.MapTo<Reading>());
						}

                        return readings;
					}
				}
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Reading>();
            }
        }

		public async Task<bool> InsertReading(Reading reading)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(_connectionString))
                using(SqlCommand cmd = new SqlCommand(StoredProcedures.InsertReading, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TimestampUtc", SqlDbType.DateTime).Value = reading.TimeStampUTC;
                    cmd.Parameters.Add("@CoinId", SqlDbType.Int).Value = reading.CoinId;
                    cmd.Parameters.Add("@PredictedClass", SqlDbType.Int).Value = reading.PredictClass;
                    cmd.Parameters.Add("@ProbSell", SqlDbType.Float).Value = reading.ProbSell;
                    cmd.Parameters.Add("@ProbHold", SqlDbType.Float).Value = reading.ProbHold;
                    cmd.Parameters.Add("@ProbBuy", SqlDbType.Float).Value = reading.ProbBuy;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = reading.Price;
                    cmd.Parameters.Add("@EMA", SqlDbType.Float).Value = reading.EMA;
                    cmd.Parameters.Add("@Volatility", SqlDbType.Float).Value = reading.Volatility;
                    cmd.Parameters.Add("@PassedProbFilter", SqlDbType.Bit).Value = reading.PassedProbFilter;
                    cmd.Parameters.Add("@PassedTrendFilter", SqlDbType.Bit).Value = reading.PassedTrendFilter;
                    cmd.Parameters.Add("@PassedVolFilter", SqlDbType.Bit).Value = reading.PassedVolFilter;
                    cmd.Parameters.Add("@FinalSignal", SqlDbType.NVarChar, 10).Value = reading.FinalSignal;
                    cmd.Parameters.Add("@ModelId", SqlDbType.Int).Value = reading.ModelId;
                    cmd.Parameters.Add("@ConfigRowId", SqlDbType.Int).Value = reading.ConfigRowId;

                    await conn.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

		public async Task<bool> InsertReadings(List<Reading> readings)
		{
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertReading, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await conn.OpenAsync();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("@TimestampUtc", typeof(DateTime));
                    dt.Columns.Add("@CoinId", typeof(int));
                    dt.Columns.Add("@PredictedClass", typeof(int));
                    dt.Columns.Add("@ProbSell", typeof(float));
                    dt.Columns.Add("@ProbHold", typeof(float));
                    dt.Columns.Add("@ProbBuy", typeof(float));
                    dt.Columns.Add("@Price", typeof(float));
                    dt.Columns.Add("@EMA", typeof(float));
                    dt.Columns.Add("@Volatility", typeof(float));
                    dt.Columns.Add("@PassedProbFilter", typeof(bool));
                    dt.Columns.Add("@PassedTrendFilter", typeof(bool));
                    dt.Columns.Add("@PassedVolFilter", typeof(bool));
                    dt.Columns.Add("@FinalSignal", typeof(string));
                    dt.Columns.Add("@ModelId", typeof(int));
                    dt.Columns.Add("@ConfigRowId", typeof(int));

                    foreach(Reading reading in readings)
                    {
                        dt.Rows.Add(
                            reading.TimeStampUTC,
                            reading.CoinId,
                            reading.PredictClass,
                            reading.ProbSell,
                            reading.ProbHold,
                            reading.ProbBuy,
                            reading.Price,
                            reading.EMA,
                            reading.Volatility,
                            reading.PassedProbFilter,
                            reading.PassedTrendFilter,
                            reading.PassedVolFilter,
                            reading.FinalSignal,
                            reading.ModelId,
                            reading.ConfigRowId
                        );
                    }

					SqlParameter param = new SqlParameter("@Readings", SqlDbType.Structured)
					{
						TypeName = "dbo.ReadingType",
						Value = dt
					};

					cmd.Parameters.Add(param);

					return await cmd.ExecuteNonQueryAsync() > 0;
				}
			}
            catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}
	}
}
