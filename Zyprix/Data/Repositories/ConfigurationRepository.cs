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

		public async Task<bool> InsertConfigurations(List<Configuration> config)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertConfigurations, conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					await conn.OpenAsync();

					var dt = new DataTable();
					dt.Columns.Add("@UniqueId", typeof(string));
					dt.Columns.Add("@BuyProbabilityThreshold", typeof(double));
					dt.Columns.Add("@SellProbabilityThreshold", typeof(double));
					dt.Columns.Add("@TrendEMALength", typeof(int));
					dt.Columns.Add("@VolFilterWindow", typeof(int));
					dt.Columns.Add("@VolMinThreshold", typeof(double));
					dt.Columns.Add("@GlobalThreshold", typeof(double));
					dt.Columns.Add("@PerSymbolFloor", typeof(double));
					dt.Columns.Add("@Margin", typeof(double));
					dt.Columns.Add("@CooldownHours", typeof(int));

					foreach (var c in config)
					{
						dt.Rows.Add(
							c.UniqueId,
							(double)c.BuyProbabilityThreshold,
							(double)c.SellProbabilityThreshold,
							c.TrendEMALenght,
							c.VolFilterWindow,
							(double)c.VolMinThreshold,
							(double)c.GlobalThreshold,
							(double)c.PerSymbolFloor,
							(double)c.Margin,
							c.CooldownHours
						);
					}

					var param = new SqlParameter("@Configurations", SqlDbType.Structured)
					{
						TypeName = "dbo.ConfigurationType",
						Value = dt
					};

					cmd.Parameters.Add(param);

					await cmd.ExecuteNonQueryAsync();
					return true;
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
