using Zyprix.Data.Interfaces;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class ReadingService : IReadingService
    {
        private readonly IReadingRepository _readingRepository;
        
        public ReadingService(IReadingRepository readingRepository)
        {
            _readingRepository = readingRepository;
        }

		public async Task<List<Reading>> GetReadings(int coinId) => await _readingRepository.GetReadings(coinId);
		public async Task<bool> InsertReading(Reading reading) => await _readingRepository.InsertReading(reading);
		public async Task<bool> InsertReadings(List<Reading> readings) => await _readingRepository.InsertReadings(readings);

    }
}
