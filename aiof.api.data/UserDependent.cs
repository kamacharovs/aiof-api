using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    /// <summary>
    /// Financial dependent of a <see cref="User"/> as they may appear on their taxes.
    /// An example of this can be a son, daughter, stepson, stepdaughter, stepborther, etc.
    /// An IRS definition of dependents can be found <see href="https://www.irs.gov/help/ita/whom-may-i-claim-as-a-dependent">here</see>
    /// </summary>
    public class UserDependent : IUserDependent,
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
        public int Age { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public decimal AmountOfSupportProvided { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserRelationship { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        [Required]
        public bool IsDeleted { get; set; } = false;
    }

    public class UserDependentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public decimal AmountOfSupportProvided { get; set; }
        public string UserRelationship { get; set; }
    }
}
