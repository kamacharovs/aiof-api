using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Goal : IGoal,
        IPublicKeyId, IIsDeleted
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public GoalType Type { get; set; }

        [Required]
        public int UserId { get; set; }

        public decimal? Amount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? MonthlyContribution { get; set; }

        [Required]
        public DateTime PlannedDate { get; set; } = DateTime.UtcNow.AddYears(1);

        public DateTime? ProjectedDate { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }

    public class GoalDto
    {
        public string Name { get; set; }
        public GoalType Type { get; set; }
        public decimal? Amount { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? MonthlyContribution { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime? ProjectedDate { get; set; }
    }
}
