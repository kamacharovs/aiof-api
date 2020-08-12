using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Goal : IEquatable<Goal>, IGoal, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; }

        [JsonIgnore]
        [Required]
        public GoalType Type { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal CurrentAmount { get; set; }

        [Required]
        public decimal Contribution { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContributionFrequency { get; set; }

        public DateTime? PlannedDate { get; set; } = DateTime.UtcNow.AddYears(1);

        [JsonIgnore]
        public int? UserId { get; set; }

        public bool Equals(Goal other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name
                && this.TypeName == other.TypeName
                && this.Amount == other.Amount;
        }

        public override bool Equals(object obj) => Equals(obj as Asset);
        public override int GetHashCode() => (Name, TypeName, Amount).GetHashCode();
    }

    public class GoalDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal Amount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal Contribution { get; set; }
        public string ContributionFrequency { get; set; }
        public DateTime? PlannedDate { get; set; }
        public int? UserId { get; set; }
    }
}
