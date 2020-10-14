using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Goal : IGoal, 
        IPublicKeyId, IPublicKeyName, IIsDeleted
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; }

        [JsonIgnore]
        public GoalType Type { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal CurrentAmount { get; set; }

        [Required]
        public decimal Contribution { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContributionFrequencyName { get; set; }

        [JsonIgnore]
        public Frequency ContributionFrequency { get; set; }

        public DateTime? PlannedDate { get; set; } = DateTime.UtcNow.AddYears(1);

        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }

    public class GoalDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal Contribution { get; set; }
        public string ContributionFrequencyName { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int? UserId { get; set; }
    }
}
