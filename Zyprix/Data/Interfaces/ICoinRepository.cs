using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface ICoinRepository
    {
        public Task<List<Coin>> GetAllCoins();
        public Task<List<Coin>> GetActiveCoins();
        public Task<Coin> GetCoin(int coinId);
	    public Task<bool> UpdateCoin(Coin coin);
		public Task<bool> UpdateCoins(List<Coin> coin);
    }
}
