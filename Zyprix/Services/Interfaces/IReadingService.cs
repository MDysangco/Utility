using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface IReadingService
    {
        public Task<List<Reading>> GetReadings(int coinId);
		public Task<bool> InsertReading(Reading reading);
		public Task<bool> InsertReadings(List<Reading> readings);

    }
}
