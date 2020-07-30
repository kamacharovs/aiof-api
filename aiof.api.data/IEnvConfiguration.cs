using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

namespace aiof.api.data
{
    public interface IEnvConfiguration
    {
        string DatabaseConString { get; }
        string MetadataDefaultFrequency { get; }

        Task<bool> IsEnabledAsync(FeatureFlags featureFlag);
    }
}
