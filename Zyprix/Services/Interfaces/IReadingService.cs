using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface IReadingService
    {
        public Task<bool> InsertReading(Reading reading);

        //TODO:
        public Task<List<Reading>> GetReadings();
        public Task<List<Reading>> GetReadings(int coinId);

    }
}
