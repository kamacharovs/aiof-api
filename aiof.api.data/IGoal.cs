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
        string TypeName { get; set; }
        [JsonIgnore] GoalType Type { get; set; }
        bool Savings { get; set; }
        [JsonIgnore] int? UserId { get; set; }
    }
}