using System;

namespace aiof.api.data
{
    public interface ILoanPayment
    {
        decimal InitialBalance { get; set; }
        decimal EndingBalance { get; set; }
        decimal Interest { get; set; }
        int Month { get; set; }
        decimal Payment { get; set; }
        decimal Principal { get; set; }
    }
}