using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        public async Task<object> PostMetadataAsync(string endpoint, string jsonContent, bool asJsonElement = false)
        {
            try
            {
                var response = await (await _client.PostAsync(endpoint, new StringContent(jsonContent, Encoding.UTF8, "application/json")))
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

        public async Task<object> GetLoanPaymentsAsync(float loanAmount, float numberOfYears, float rateOfInterest, string frequency = "monthly")
        {
            _logger.LogInformation($"getting loan payments information. loanAmount='{loanAmount}', numberOfYears='{numberOfYears}', " +
                $"rateOfInterest='{rateOfInterest}', frequency='{frequency}'");

            return await PostMetadataAsync($"loan/payments/{frequency}",
                JsonSerializer.Serialize(new
                {
                    loanAmount,
                    numberOfYears,
                    rateOfInterest
                }));
        }
    }
}
