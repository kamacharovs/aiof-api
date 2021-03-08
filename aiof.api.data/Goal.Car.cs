using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    /// <summary>
    /// The <see cref="Goal.Amount"/> should be calculated based on the 
    /// <see cref="DesiredMonthlyPayment"/>, 
    /// <see cref="LoanTermMonths"/> and 
    /// <see cref="InterestRate"/>.
    /// That would be the down payment needed to initiate and start the loan
    /// </summary>
    public class GoalCar : Goal
    {
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public bool? New { get; set; } = false;
        public decimal? DesiredMonthlyPayment { get; set; }
        public int? LoanTermMonths { get; set; } = 36;
        public decimal? InterestRate { get; set; } = 2.75M;
    }

    public class GoalCarDto : GoalDto
    {
        public int? Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public bool? New { get; set; }
        public decimal? DesiredMonthlyPayment { get; set; }
        public int? LoanTermMonths { get; set; }
        public decimal? InterestRate { get; set; }
    }
}
