using System;
using System.Collections.Generic;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class User : IUser, 
        IPublicKeyId
    {
        [Required]
        public int Id { get; set; }

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
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [Required]
        public UserProfile Profile { get; set; }

        public ICollection<UserDependent> Dependents { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
        public ICollection<Goal> Goals { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }

    public class UserDto
    {
        public ICollection<AssetDto> Assets { get; set; }
        public ICollection<GoalDto> Goals { get; set; }
        public ICollection<LiabilityDto> Liabilities { get; set; }
        public ICollection<SubscriptionDto> Subscriptions { get; set; }
        public ICollection<AccountDto> Accounts { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
