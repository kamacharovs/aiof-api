using System;

namespace aiof.api.data
{
    public interface ILoanPayment
    {
        float InitialBalance { get; set; }
        float EndingBalance { get; set; }
        float Interest { get; set; }
        int Month { get; set; }
        float Payment { get; set; }
        float Principal { get; set; }
    }
}