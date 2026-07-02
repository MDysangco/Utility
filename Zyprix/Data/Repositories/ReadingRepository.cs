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
					cmd.CommandTimeout = 120;
					SqlRetry.Apply(conn, cmd);
					cmd.Parameters.Add("@CoinId", SqlDbType.Int).Value = coinId;

                    await conn.OpenAsync();

					using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
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

		public async Task<bool> InsertReadings(List<Reading> readings)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertReadings, conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					SqlRetry.Apply(conn, cmd);
					await conn.OpenAsync();

					DataTable dt = new DataTable();
					dt.Columns.Add("@TimestampUtc", typeof(DateTime));
					dt.Columns.Add("@CoinId", typeof(int));
					dt.Columns.Add("@PredictedClass", typeof(int));
					dt.Columns.Add("@ProbSell", typeof(double));
					dt.Columns.Add("@ProbHold", typeof(double));
					dt.Columns.Add("@ProbBuy", typeof(double));
					dt.Columns.Add("@Price", typeof(double));
					dt.Columns.Add("@EMA", typeof(double));
					dt.Columns.Add("@Volatility", typeof(double));
					dt.Columns.Add("@PassedProbFilter", typeof(bool));
					dt.Columns.Add("@PassedTrendFilter", typeof(bool));
					dt.Columns.Add("@PassedVolFilter", typeof(bool));
					dt.Columns.Add("@FinalSignal", typeof(string));
					dt.Columns.Add("@ModelId", typeof(int));
					dt.Columns.Add("@ConfigUniqueId", typeof(string));
					dt.Columns.Add("@SentToAzure", typeof(bool));

					foreach (Reading r in readings)
					{
						dt.Rows.Add(
							r.TimeStampUTC,
							r.CoinId,
							r.PredictClass,
							r.ProbSell,
							r.ProbHold,
							r.ProbBuy,
							r.Price,
							r.EMA,
							r.Volatility,
							r.PassedProbFilter,
							r.PassedTrendFilter,
							r.PassedVolFilter,
							r.FinalSignal,
							r.ModelId,
							r.ConfigUniqueId,
							r.SentToAzure
						);
					}

					SqlParameter param = new SqlParameter("@Readings", SqlDbType.Structured)
					{
						TypeName = "dbo.ReadingType",
						Value = dt
					};

					cmd.Parameters.Add(param);

					await cmd.ExecuteNonQueryAsync();
					return true;
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
