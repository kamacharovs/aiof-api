using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface ILiabilityType
    {
        [Required]
        [MaxLength(100)]
        string Name { get; set; }
        
        [JsonIgnore]
        [Required]
        Guid PublicKey { get; set; }
    }
}