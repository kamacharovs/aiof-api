using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aiof.api.data
{
    public interface IEnvConfiguration
    {
        Task<bool> IsEnabledAsync(FeatureFlags featureFlag);
    }
}
