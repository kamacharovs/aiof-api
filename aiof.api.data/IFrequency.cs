using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IFrequency
    {
        [JsonIgnore]
        [Required]
        int Id { get; set; }

        [JsonIgnore]
        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(20)]
        string Name { get; set; }

        /// <summary>
        /// Value compared to a Year. For example, a Day is 365, a Week is 52, a Month is 12, etc. This will also be used to support custom frequencies
        /// </summary>
        [Required]
        int Value { get; set; }
    }
}