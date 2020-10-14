﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Asset : IAsset, 
        IPublicKeyId, IPublicKeyName, IIsDeleted
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; }

        [JsonIgnore]
        public AssetType Type { get; set; }

        [Required]
        public decimal Value { get; set; } 

        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;
    }

    public class AssetDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? UserId { get; set; }
    }
}
