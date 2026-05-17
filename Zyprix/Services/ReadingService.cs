using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<bool> InsertReading(Reading reading) => await _readingRepository.InsertReading(reading);

        //TODO:
        public async Task<List<Reading>> GetReadings() => await _readingRepository.GetReadings();

        public async Task<List<Reading>> GetReadings(int coinId) => await _readingRepository.GetReadings(coinId);

    }
}
