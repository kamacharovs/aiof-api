using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Address
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string StreetLine1 { get; set; }

        [Required]
        [MaxLength(200)]
        public string StreetLine2 { get; set; }

        [Required]
        [MaxLength(200)]
        public string City { get; set; }

        [Required]
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(5)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Country { get; set; } = "USA";

        [Required]
        public int UserProfileId { get; set; }
    }
}
