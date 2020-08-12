using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public interface IGoal
    {
        [JsonIgnore] int Id { get; set; }
        [JsonIgnore] Guid PublicKey { get; set; }
        string Name { get; set; }
        decimal Amount { get; set; }
        decimal CurrentAmount { get; set; }
        decimal? Contribution { get; set; }
        string ContributionFrequency { get; set; }
        string TypeName { get; set; }
        [JsonIgnore] GoalType Type { get; set; }
        DateTime? PlannedDate { get; set; }
        [JsonIgnore] int? UserId { get; set; }
    }
}