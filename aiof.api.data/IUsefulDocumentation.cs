using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IUsefulDocumentation
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(100)]
        string Page { get; set; }

        [Required]
        [MaxLength(300)]
        string Name { get; set; }

        [Required]
        [MaxLength(500)]
        string Url { get; set; }

        [MaxLength(100)]
        string Category { get; set; }
    }
}