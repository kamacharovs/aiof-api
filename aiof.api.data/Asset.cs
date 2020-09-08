using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Asset : IEquatable<Asset>, IAsset, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore]
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


        public bool Equals(Asset other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name 
                && this.TypeName == other.TypeName
                && this.Value == other.Value;
        }

        public override bool Equals(object obj) => Equals(obj as Asset);
        public override int GetHashCode() => (Name, TypeName, Value).GetHashCode();
    }

    public class AssetDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? UserId { get; set; }
    }
}
