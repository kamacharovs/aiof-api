using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IUser
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(200)]
        string FirstName { get; set; }

        [Required]
        [MaxLength(200)]
        string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        string Email { get; set; }

        [Required]
        DateTime Created { get; set; }

        [Required]
        UserProfile Profile { get; set; }

        ICollection<UserDependent> Dependents { get; set; }
        ICollection<Asset> Assets { get; set; }
        ICollection<Goal> Goals { get; set; }
        ICollection<Liability> Liabilities { get; set; }
        ICollection<Subscription> Subscriptions { get; set; }
        ICollection<Account> Accounts { get; set; }
    }
}