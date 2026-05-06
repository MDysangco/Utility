using Microsoft.Data.SqlClient;
using System.Data;
using Utils;
using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services;

namespace Zyprix.Data.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly string _connectionString;

        public CoinRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Coin> GetAllCoins()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllCoins, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coins.Add(reader.MapTo<Coin>());
                        }
                    }

                    return coins;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Coin>();
            }
        }

        public IEnumerable<Coin> GetActiveCoins()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetActiveCoins, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            coins.Add(reader.MapTo<Coin>());
                        }
                    }

                    return coins;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Coin>();
            }
        }

        public Coin GetCoin(int Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCoin, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = Id;
                    conn.Open();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.MapTo<Coin>();
                        }
                    }

                    return new Coin();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Coin();
            }
        }

        public bool UpdateCoin(Coin coin, bool active, long binanceListingDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("UpdateCoin", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = coin.Id;
                    cmd.Parameters.Add("Active", SqlDbType.Bit).Value = active;
                    cmd.Parameters.Add("BinanceListingDate", SqlDbType.Decimal).Value = binanceListingDate;
                    conn.Open();

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Coin CreateCoin(Coin coin)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCoin(int coinId)
        {
            throw new NotImplementedException();
        }
    }
}
