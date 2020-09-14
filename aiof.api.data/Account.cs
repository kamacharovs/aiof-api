using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Account : IAccount,
        IPublicKeyId
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}