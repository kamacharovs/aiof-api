using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface ISubscription
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(200)]
        string Name { get; set; }

        [MaxLength(500)]
        string Description { get; set; }

        [Required]
        decimal Amount { get; set; }

        [Required]
        [MaxLength(20)]
        string PaymentFrequencyName { get; set; }

        [JsonIgnore]
        Frequency PaymentFrequency { get; set; }
        
        [Required]
        int PaymentLength { get; set; }

        [MaxLength(200)]
        string From { get; set; }

        [MaxLength(500)]
        string Url { get; set; }

        [JsonIgnore]
        [Required]
        int UserId { get; set; }
    }
}