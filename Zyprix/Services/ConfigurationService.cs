using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Data.Interfaces;
using Zyprix.Data.Repositories;
using Zyprix.Models;
using Zyprix.Services.Interfaces;

namespace Zyprix.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            _configurationRepository = configurationRepository;
        }


        public async Task<int> InsertConfiguration(Configuration config) => await _configurationRepository.InsertConfiguration(config);

        //TODO:

        public async Task<Configuration> GetConfiguration() => await _configurationRepository.GetConfiguration();

        public async Task<List<Configuration>> GetConfigurations() => await _configurationRepository.GetConfigurations();

    }
}
