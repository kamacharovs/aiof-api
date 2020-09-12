using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IAccount
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        string Name { get; set; }

        string Description { get; set; }

        [Required]
        string TypeName { get; set; }

        [Required]
        int UserId { get; set; }
    }
}