using System;
using System.Collections.Generic;
using System.Text;
using Zyprix.Models;

namespace Zyprix.Services.Interfaces
{
    public interface IConfigurationService
    {
		public Task<bool> InsertConfigurations(List<Configuration> configs);
    }
}
