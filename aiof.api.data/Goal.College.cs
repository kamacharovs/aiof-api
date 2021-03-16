using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class GoalCollege : Goal
    {
        /// <summary>
        /// College type. For example, Public (in state), Public (out of state), etc.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public GoalCollegeType CollegeType { get; set; }

        /// <summary>
        /// Cost per year in today's dollars
        /// </summary>
        [Required]
        public decimal CostPerYear { get; set; }

        /// <summary>
        /// Student's current age
        /// </summary>
        [Required]
        public int StudentAge { get; set; }

        /// <summary>
        /// Years in college
        /// </summary>
        [Required]
        public int Years { get; set; }

        /// <summary>
        /// Annual cost of increase
        /// </summary>
        public decimal? AnnualCostIncrease { get; set; } = 0.04M;
        /// <summary>
        /// The age the student will begin college
        /// </summary>
        public int? BeginningCollegeAge { get; set; } = 18;
    }

    public class GoalCollegeDto : GoalDto
    {
        public string CollegeType { get; set; }
        public decimal CostPerYear { get; set; }
        public int StudentAge { get; set; }
        public int Years { get; set; }
        public decimal? AnnualCostIncrease { get; set; }
        public int? BeginningCollegeAge { get; set; }
    }
}
