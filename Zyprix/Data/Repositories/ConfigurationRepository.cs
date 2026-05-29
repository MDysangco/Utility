using Microsoft.Data.SqlClient;
using System.Data;
using Zyprix.Data.Interfaces;
using Zyprix.Models;

namespace Zyprix.Data.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly string _connectionString;

        public ConfigurationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

		public async Task<int> InsertConfiguration(Configuration config)
        {
            try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertConfiguration, conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@BuyProbabilityThreshold", SqlDbType.Float).Value = (double)config.BuyProbabilityThreshold;
					cmd.Parameters.Add("@SellProbabilityThreshold", SqlDbType.Float).Value = (double)config.SellProbabilityThreshold;
					cmd.Parameters.Add("@TrendEMALength", SqlDbType.Int).Value = config.TrendEMALenght;
					cmd.Parameters.Add("@VolFilterWindow", SqlDbType.Int).Value = config.VolFilterWindow;
					cmd.Parameters.Add("@VolMinThreshold", SqlDbType.Float).Value = (double)config.VolMinThreshold;
					cmd.Parameters.Add("@GlobalThreshold", SqlDbType.Float).Value = (double)config.GlobalThreshold;
					cmd.Parameters.Add("@PerSymbolFloor", SqlDbType.Float).Value = (double)config.PerSymbolFloor;
					cmd.Parameters.Add("@Margin", SqlDbType.Float).Value = (double)config.Margin;
					cmd.Parameters.Add("@CooldownHours", SqlDbType.Int).Value = config.CooldownHours;

					var outParam = cmd.Parameters.Add("@NewId", SqlDbType.Int);
					outParam.Direction = ParameterDirection.Output;

					await conn.OpenAsync();
					await cmd.ExecuteNonQueryAsync();
					return (int)outParam.Value;
				}

			}
            catch (Exception ex)
            { 
				Console.WriteLine($"Error inserting configuration: {ex.Message}");
				return -1;
			}
        }

		public async Task<bool> InsertConfigurations(List<Configuration> config)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertConfigurations, conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					await conn.OpenAsync();

					var dt = new DataTable();
					dt.Columns.Add("BuyProbabilityThreshold", typeof(double));
					dt.Columns.Add("SellProbabilityThreshold", typeof(double));
					dt.Columns.Add("TrendEMALength", typeof(int));
					dt.Columns.Add("VolFilterWindow", typeof(int));
					dt.Columns.Add("VolMinThreshold", typeof(double));
					dt.Columns.Add("GlobalThreshold", typeof(double));
					dt.Columns.Add("PerSymbolFloor", typeof(double));
					dt.Columns.Add("Margin", typeof(double));
					dt.Columns.Add("CooldownHours", typeof(int));

					foreach (var configItem in config)
					{
						dt.Rows.Add(
							(double)configItem.BuyProbabilityThreshold,
							(double)configItem.SellProbabilityThreshold,
							configItem.TrendEMALenght,
							configItem.VolFilterWindow,
							(double)configItem.VolMinThreshold,
							(double)configItem.GlobalThreshold,
							(double)configItem.PerSymbolFloor,
							(double)configItem.Margin,
							configItem.CooldownHours
						);
					}

					SqlParameter param = new SqlParameter("@Configurations", SqlDbType.Structured)
					{
						TypeName = "dbo.ConfigurationType",
						Value = dt
					};

					cmd.Parameters.Add(param);
					return await cmd.ExecuteNonQueryAsync() > 0;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error inserting configurations: {ex.Message}");
				return false;
			}
		}
	}
}
