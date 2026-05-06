using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Utils;
using Zyprix.Data.Interfaces;
using Zyprix.Models;

namespace Zyprix.Data.Repositories
{
    public class KlineRepository : IKlineRepository
    {
        private readonly string _connectionString;

        public KlineRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int DeleteKlinesByDateRange(long startDate, long endDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteKlinesByDateRange, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("StartDate", SqlDbType.Decimal).Value = startDate;
                    cmd.Parameters.Add("EndDate", SqlDbType.Decimal).Value = endDate;
                    conn.Open();

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public Kline GetEarliestRecordedKline(Coin coin, KlineInterval interval)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetEarliestRecordedKline, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = coin.Id;
                    cmd.Parameters.Add("Interval", SqlDbType.Int).Value = (int)interval;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.MapTo<Kline>();
                        }
                    }
                }

                return new Kline();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Kline();
            }
        }

        public Kline GetLatestRecordedKline(Coin coin, KlineInterval interval)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetLatestRecordedKline, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = coin.Id;
                    cmd.Parameters.Add("Interval", SqlDbType.Int).Value = (int)interval;
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.MapTo<Kline>();
                        }
                    }
                }

                return new Kline();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Kline();
            }
        }

        public bool InsertKlines(List<Kline> klines)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.InsertKlines, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("@CoinId", typeof(int));
                    dt.Columns.Add("@Interval", typeof(int));
                    dt.Columns.Add("@KlineOpenTime", typeof(string));
                    dt.Columns.Add("@OpenPrice", typeof(decimal));
                    dt.Columns.Add("@HighPrice", typeof(decimal));
                    dt.Columns.Add("@LowPrice", typeof(decimal));
                    dt.Columns.Add("@ClosePrice", typeof(decimal));
                    dt.Columns.Add("@Volume", typeof(decimal));
                    dt.Columns.Add("@NumberOfTrades", typeof(int));

                    foreach (Kline kline in klines)
                    {
                        dt.Rows.Add(
                            kline.CoinId,
                            kline.Interval,
                            kline.KlineOpenTime,
                            kline.OpenPrice,
                            kline.HighPrice,
                            kline.LowPrice,
                            kline.ClosePrice,
                            kline.Volume,
                            kline.NumberOfTrades
                        );
                    }

                    SqlParameter param = new SqlParameter("@Klines", SqlDbType.Structured)
                    {
                        TypeName = "dbo.KlineType",
                        Value = dt
                    };

                    cmd.Parameters.Add(param);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public IEnumerable<Kline> GetKlines()
        {
            throw new NotImplementedException();
        }



    }
}
