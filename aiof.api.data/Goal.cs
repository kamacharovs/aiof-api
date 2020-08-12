using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public class Goal : IEquatable<Goal>, IGoal, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public decimal CurrentAmount { get; set; }

        public decimal? Contribution { get; set; }

        public string ContributionFrequency { get; set; }

        public string TypeName { get; set; }

        [JsonIgnore]
        public GoalType Type { get; set; }

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
        public bool? Savings { get; set; }
        public int? UserId { get; set; }
    }
}
