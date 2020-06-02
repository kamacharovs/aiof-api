using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<object> GetMetadataAsync(string endpoint, bool asJsonElement = false)
        {
            try
            {
                var response = await (await _client.GetAsync(endpoint))
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync();

                var responseObj = JsonSerializer.Deserialize<object>(response);

                return asJsonElement
                    ? (JsonElement)responseObj
                    : responseObj;
            }
            catch (Exception e)
            {
                throw new AiofFriendlyException(e.Message);
            }
        }

        public async Task<object> GetFrequenciesAsync()
        {
            return await GetMetadataAsync("frequencies");
        }
    }
}
