using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Frequency : IFrequency,
        IPublicKeyName
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Value compared to a Year. For example, a Day is 365, a Week is 52, a Month is 12, etc. This will also be used to support custom frequencies
        /// </summary>
        [JsonIgnore]
        [Required]
        public int Value { get; set; }
    }
}
