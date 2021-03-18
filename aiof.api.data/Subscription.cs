using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Subscription : ISubscription,
        IPublicKeyId, IPublicKeyName, IIsDeleted
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }
        
        [Required]
        public int PaymentLength { get; set; }

        [MaxLength(200)]
        public string From { get; set; }

        [MaxLength(500)]
        public string Url { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }

    public class SubscriptionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentFrequencyName { get; set; }
        public int? PaymentLength { get; set; }
        public string From { get; set; }
        public string Url { get; set; }
    }
}