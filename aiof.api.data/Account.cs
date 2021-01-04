using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Account : IAccount,
        IPublicKeyId, IIsDeleted
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; }

        [Required]
        public AccountType Type { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }

    public class AccountDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
    }
}