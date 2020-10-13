using System;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class LiabilityType : ILiabilityType, 
        IPublicKeyName
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();
    }
}
