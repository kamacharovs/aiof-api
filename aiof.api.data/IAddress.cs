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

        [MaxLength(200)]
        string StreetLine1 { get; set; }

        [MaxLength(200)]
        string StreetLine2 { get; set; }

        [MaxLength(200)]
        string City { get; set; }

        [MaxLength(2)]
        string State { get; set; }

        [MaxLength(5)]
        string ZipCode { get; set; }

        [MaxLength(200)]
        string Country { get; set; }

        [Required]
        int UserProfileId { get; set; }
    }
}