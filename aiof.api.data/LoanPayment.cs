using System;

namespace aiof.api.data
{
    public class LoanPayment : ILoanPayment
    {
        public float InitialBalance { get; set; }
        public float EndingBalance { get; set; }
        public float Interest { get; set; }
        public int Month { get; set; }
        public float Payment { get; set; }
        public float Principal { get; set; }
    }
}