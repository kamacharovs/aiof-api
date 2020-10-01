using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IType
    {
        [Required]
        string Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }
        
        [Required]
        [MaxLength(100)]
        string Name { get; set; }

        [Required]
        [MaxLength(500)]
        string Description { get; set; }
    }
}