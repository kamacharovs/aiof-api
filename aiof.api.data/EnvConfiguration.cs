using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace aiof.api.data
{
    public class EnvConfiguration : IEnvConfiguration
    {
        public IConfiguration _configuration { get; }

        public EnvConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public class OpenApi
        {
            private IEnvConfiguration _envConfiguration { get; }

            public OpenApi(IEnvConfiguration envConfiguration)
            {
                _envConfiguration = envConfiguration ?? throw new ArgumentNullException(nameof(envConfiguration));
            }

            public string Version => _envConfiguration._configuration["OpenApi:Version"];
            public string Title => _envConfiguration._configuration["OpenApi:Title"];
            public string Description => _envConfiguration._configuration["OpenApi:Description"];
            public string ContactName => _envConfiguration._configuration["OpenApi:Contact:Name"];
            public string ContactEmail => _envConfiguration._configuration["OpenApi:Contact:Email"];
            public string ContactUrl => _envConfiguration._configuration["OpenApi:Contact:Url"];
            public string LicenseName => _envConfiguration._configuration["OpenApi:License:Name"];
            public string LicenseUrl => _envConfiguration._configuration["OpenApi:License:Url"];
        }
    }
}
