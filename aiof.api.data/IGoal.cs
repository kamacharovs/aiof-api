using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IGoal
    {
        [JsonIgnore]
        [Required]
        int Id { get; set; }

        [JsonIgnore]
        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [Required]
        [MaxLength(100)]
        string TypeName { get; set; }

        [JsonIgnore]
        [Required]
        GoalType Type { get; set; }

        [Required]
        decimal Amount { get; set; }

        [Required]
        decimal CurrentAmount { get; set; }

        [Required]
        decimal Contribution { get; set; }

        [Required]
        [MaxLength(20)]
        string ContributionFrequency { get; set; }

        DateTime? PlannedDate { get; set; }

        [JsonIgnore]
        int? UserId { get; set; }
    }
}