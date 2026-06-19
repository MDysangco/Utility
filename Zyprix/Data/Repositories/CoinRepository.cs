using Microsoft.Data.SqlClient;
using System.Data;
using Utils;
using Zyprix.Data.Interfaces;
using Zyprix.Models;

namespace Zyprix.Data.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private readonly string _connectionString;

        public CoinRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Coin>> GetAllCoins()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllCoins, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;
                    await conn.OpenAsync();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public async Task<List<Coin>> GetActiveCoins()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetActiveCoins, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					await conn.OpenAsync();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public async Task<Coin> GetCoin(int Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.GetCoin, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = Id;
                    await conn.OpenAsync();

                    List<Coin> coins = new List<Coin>();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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

        public async Task<bool> UpdateCoin(Coin coin)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateCoin, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					cmd.Parameters.Add("CoinId", SqlDbType.Int).Value = coin.Id;
                    cmd.Parameters.Add("Active", SqlDbType.Bit).Value = coin.Active;
                    cmd.Parameters.Add("BinanceListingDate", SqlDbType.Decimal).Value = coin.BinanceListingDate;
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

        public async Task<bool> UpdateCoins(List<Coin> coins)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				using (SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateCoins, conn))
				{
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.CommandTimeout = 120;
					await conn.OpenAsync();

					DataTable dt = new DataTable();
                    dt.Columns.Add("@Id", typeof(int));
                    dt.Columns.Add("@Ticker", typeof(string));
                    dt.Columns.Add("@Name", typeof(string));
                    dt.Columns.Add("@Address", typeof(string));
                    dt.Columns.Add("@ChainId", typeof(int));
                    dt.Columns.Add("@Active", typeof(bool));
                    dt.Columns.Add("@BinanceListingDate", typeof(long));

					foreach (var coin in coins)
					{
                        dt.Rows.Add(
                            coin.Id, 
                            coin.Ticker, 
                            coin.Name, 
                            coin.Address, 
                            coin.ChainId, 
                            coin.Active, 
                            coin.BinanceListingDate
                        );
					}

					SqlParameter param = new SqlParameter("@Coins", SqlDbType.Structured)
					{
						TypeName = "dbo.CoinType",
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
