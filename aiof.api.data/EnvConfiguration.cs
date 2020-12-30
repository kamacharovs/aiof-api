using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace aiof.api.data
{
    public class EnvConfiguration : IEnvConfiguration
    {
        public readonly IConfiguration _config;
        public readonly IFeatureManager _featureManager;

        public EnvConfiguration(
            IConfiguration config,
            IFeatureManager featureManager)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _featureManager = featureManager ?? throw new ArgumentNullException(nameof(featureManager));
        }

        public async Task<bool> IsEnabledAsync(FeatureFlags featureFlag)
        {
            return await _featureManager.IsEnabledAsync(nameof(featureFlag));
        }
    }

    public enum FeatureFlags
    {
        Asset,
        Goal,
        Liability,
        Account,
        UserDependent
    }
}
