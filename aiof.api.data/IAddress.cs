using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IAddress
    {
        [Required]
        int Id { get; set; }

        [Required]
        Guid PublicKey { get; set; }

        [Required]
        [MaxLength(200)]
        string StreetLine1 { get; set; }

        [Required]
        [MaxLength(200)]
        string StreetLine2 { get; set; }

        [Required]
        [MaxLength(200)]
        string City { get; set; }

        [Required]
        [MaxLength(2)]
        string State { get; set; }

        [Required]
        [MaxLength(5)]
        string ZipCode { get; set; }

        [Required]
        [MaxLength(200)]
        string Country { get; set; }

        [Required]
        int UserProfileId { get; set; }
    }
}