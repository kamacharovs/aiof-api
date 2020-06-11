using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace aiof.api.data
{
    public class Asset : IAsset, IPublicKeyId
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public AssetType Type { get; set; }
        public float Value { get; set; } 
        public int FinanceId { get; set; }
    }

    public class AssetDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Required]
        public float Value { get; set; }

        public int FinanceId { get; set; }
    }
}
