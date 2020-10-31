using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface ILiability
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [Required]
        [MaxLength(100)]
        string TypeName { get; set; }

        [JsonIgnore]
        [Required]
        LiabilityType Type { get; set; }

        [Required]
        decimal Value { get; set; }

        decimal? MonthlyPayment { get; set; }

        int? Years { get; set; }

        [JsonIgnore]
        int UserId { get; set; }

        [JsonIgnore]
        bool IsDeleted { get; set; }
    }
}