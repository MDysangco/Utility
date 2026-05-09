using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface IReadingRepository
    {
        public Task<int> InsertReading(Reading reading);
        public Task<int> InsertReadings(List<Reading> readings);

        //TODO:
        public Task<List<Reading>> GetReadings();
        public Task<List<Reading>> GetReadings(int coinId);
        
    }

    public interface IConfigurationRepository
    {

    }   
}
