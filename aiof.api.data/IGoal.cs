using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IGoal
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        GoalType Type { get; set; }

        [Required]
        int UserId { get; set; }

        decimal? Amount { get; set; }
        decimal? CurrentAmount { get; set; }
        decimal? MonthlyContribution { get; set; }

        [Required]
        DateTime PlannedDate { get; set; } 

        DateTime? ProjectedDate { get; set; }

        [JsonIgnore]
        bool IsDeleted { get; set; }
    }
}