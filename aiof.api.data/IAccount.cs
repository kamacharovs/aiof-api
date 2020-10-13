using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IAccount
    {
        [JsonIgnore]
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(200)]
        string Name { get; set; }

        [Required]
        [MaxLength(500)]
        string Description { get; set; }

        [Required]
        [MaxLength(100)]
        string TypeName { get; set; }

        [JsonIgnore]
        [Required]
        int UserId { get; set; }
        
        [JsonIgnore]
        bool IsDeleted { get; set; }
    }
}