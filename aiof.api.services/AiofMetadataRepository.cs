using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

using Microsoft.Extensions.Logging;

using aiof.api.data;

namespace aiof.api.services
{
    public class AiofMetadataRepository : IAiofMetadataRepository
    {
        private readonly HttpClient _client;
        private readonly ILogger<AiofMetadataRepository> _logger;

        public AiofMetadataRepository(HttpClient client, ILogger<AiofMetadataRepository> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task SendMetadataAsync()
        {
            var response = await _client.GetStringAsync("frequencies");
            var k = 0;
        }
    }
}
