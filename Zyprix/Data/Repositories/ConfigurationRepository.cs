using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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


        public Task<Configuration> GetConfiguration()
        {
            throw new NotImplementedException();
        }

        public Task<List<Configuration>> GetConfigurations()
        {
            throw new NotImplementedException();
        }


    }
}
