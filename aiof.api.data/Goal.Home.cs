using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class GoalHome : Goal
    {
        public decimal? HomeValue { get; set; }
        public decimal? MortgageRate { get; set; }
        public decimal? PercentDownPayment { get; set; }
        public decimal? AnnualInsurance { get; set; }
        public decimal? AnnualPropertyTax { get; set; }
        public decimal? RecommendedAmount { get; set; }
    }

    public class GoalHomeDto : GoalDto
    {
        public decimal? HomeValue { get; set; }
        public decimal? MortgageRate { get; set; }
        public decimal? PercentDownPayment { get; set; }
        public decimal? AnnualInsurance { get; set; }
        public decimal? AnnualPropertyTax { get; set; }
        public decimal? RecommendedAmount { get; set;  }
    }
}
