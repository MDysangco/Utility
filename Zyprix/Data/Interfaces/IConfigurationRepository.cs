using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Data.Interfaces
{
    public interface IConfigurationRepository
    {
		public Task<int> InsertConfiguration(Configuration config);
		public Task<bool> InsertConfigurations(List<Configuration> config);
    }
}
