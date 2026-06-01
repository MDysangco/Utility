using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface IReadingRepository
    {
		public Task<List<Reading>> GetReadings(int coinId);
		public Task<bool> InsertReadings(List<Reading> readings);

    }

}
