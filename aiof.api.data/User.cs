using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class User : IUser, 
        IPublicKeyId
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Username { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Required]
        public UserProfile Profile { get; set; }

        public ICollection<Asset> Assets { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }

    public class UserDto
    {
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
    }

    public class UserDto2
    {
        public ICollection<AssetDto> Assets { get; set; }
        public ICollection<GoalDto> Goals { get; set; }
        public ICollection<LiabilityDto> Liabilities { get; set; }
        public ICollection<SubscriptionDto> Subscriptions { get; set; }
    }
}
