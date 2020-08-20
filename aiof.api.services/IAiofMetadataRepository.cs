﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofMetadataRepository
    {
        Task<string> GetMetadataAsync(string endpoint);
        Task<string> PostMetadataAsync(
            string endpoint, 
            string jsonContent);
        Task<IEnumerable<string>> GetFrequenciesAsync();
        Task<IEnumerable<ILoanPayment>> GetLoanPaymentsAsync(
            decimal loanAmount, 
            decimal numberOfYears, 
            decimal rateOfInterest, 
            string frequency = "monthly");
    }
}
