using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Polly;

namespace aiof.api.data
{
    public interface IEnvConfiguration
    {
        string DataPostgreSQL { get; }
        int PollyDefaultRetry { get; }
        string MetadataDefaultFrequency { get; }

        IAsyncPolicy<HttpResponseMessage> DefaultRetryPolicy(ILogger logger);

        Task<bool> IsEnabledAsync(FeatureFlags featureFlag);
    }
}
