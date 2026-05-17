using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text;
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


        //TODO:
        public Task<List<Reading>> GetReadings()
        {
            throw new NotImplementedException();
        }

        public Task<List<Reading>> GetReadings(int coinId)
        {
            throw new NotImplementedException();
        }

    }
}
