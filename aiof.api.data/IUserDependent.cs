using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IUserDependent
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
        int Age { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        string Email { get; set; }

        [Required]
        decimal AmountOfSupportProvided { get; set; }

        [Required]
        [MaxLength(100)]
        string UserRelationship { get; set; }

        [Required]
        int UserId { get; set; }

        [Required]
        DateTime Created { get; set; }

        [JsonIgnore]
        [Required]
        bool IsDeleted { get; set; }
    }
}
