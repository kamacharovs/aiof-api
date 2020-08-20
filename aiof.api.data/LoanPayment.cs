using System;

namespace aiof.api.data
{
    public class LoanPayment : ILoanPayment
    {
        public decimal InitialBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal Interest { get; set; }
        public int Month { get; set; }
        public decimal Payment { get; set; }
        public decimal Principal { get; set; }
    }
}