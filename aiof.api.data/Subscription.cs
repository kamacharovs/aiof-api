using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Subscription :
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentFrequencyName { get; set; }

        [JsonIgnore]
        public Frequency PaymentFrequency { get; set; }
        
        [Required]
        public int PaymentLength { get; set; }

        [MaxLength(200)]
        public string From { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }
    }
}