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
        private readonly ILogger<AiofMetadataRepository> _logger;
        private readonly IEnvConfiguration _envConfig;
        private readonly HttpClient _client;

        public AiofMetadataRepository(
            ILogger<AiofMetadataRepository> logger, 
            IEnvConfiguration envConfig,
            HttpClient client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _envConfig = envConfig ?? throw new ArgumentNullException(nameof(envConfig));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetMetadataAsync(string endpoint)
        {
            try
            {
                return await (await _client.GetAsync(endpoint))
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new AiofFriendlyException(e.Message);
            }
        }

        public async Task<string> PostMetadataAsync(
            string endpoint, 
            string jsonContent)
        {
            try
            {
                return await (await _client.PostAsync(endpoint, new StringContent(jsonContent, Encoding.UTF8, "application/json")))
                    .EnsureSuccessStatusCode()
                    .Content
                    .ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new AiofFriendlyException(e.Message);
            }
        }

        public async Task<IEnumerable<string>> GetFrequenciesAsync()
        {
            return JsonSerializer.Deserialize<List<string>>(
                await GetMetadataAsync("frequencies"));
        }

        public async Task<IEnumerable<ILoanPayment>> GetLoanPaymentsAsync(
            decimal loanAmount, 
            decimal numberOfYears, 
            decimal rateOfInterest, 
            string frequency = null)
        {
            frequency = frequency ?? _envConfig.MetadataDefaultFrequency;
            
            _logger.LogInformation($"getting loan payments information. loanAmount='{loanAmount}', numberOfYears='{numberOfYears}', " +
                $"rateOfInterest='{rateOfInterest}', frequency='{frequency}'");

            return JsonSerializer.Deserialize<List<LoanPayment>>(
                await PostMetadataAsync($"loan/payments/{frequency}",
                JsonSerializer.Serialize(new
                {
                    loanAmount,
                    numberOfYears,
                    rateOfInterest
                })));
        }
    }
}
