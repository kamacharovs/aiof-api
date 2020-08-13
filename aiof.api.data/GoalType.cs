using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class GoalType : IGoalType, 
        IPublicKeyName
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public Guid PublicKey { get; set; } = Guid.NewGuid();
    }
}
