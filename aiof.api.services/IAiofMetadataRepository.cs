using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aiof.api.services
{
    public interface IAiofMetadataRepository
    {
        Task<string> GetMetadataAsync(string endpoint, bool asJsonElement = false);
        Task<object> PostMetadataAsync(string endpoint, string jsonContent, bool asJsonElement = false);
        Task<IEnumerable<string>> GetFrequenciesAsync();
        Task<object> GetLoanPaymentsAsync(double loanAmount, double numberOfYears, double rateOfInterest, string frequency = "monthly");
    }
}
