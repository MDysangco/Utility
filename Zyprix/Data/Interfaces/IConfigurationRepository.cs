using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface IConfigurationRepository
    {
        public Task<int> InsertConfiguration(Configuration config);

        //TODO:
        public Task<Configuration> GetConfiguration();
        public Task<List<Configuration>> GetConfigurations();
    }
}
