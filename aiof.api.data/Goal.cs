using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public class Goal : IGoal, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TypeName { get; set; }
        [JsonIgnore] public GoalType Type { get; set; }
        public bool Savings { get; set; } = false;
        public int? UserId { get; set; }
    }

    public class GoalDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool? Savings { get; set; }
        public int? UserId { get; set; }
    }
}
