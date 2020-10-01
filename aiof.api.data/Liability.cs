using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Liability : IEquatable<Liability>, ILiability, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string TypeName { get; set; }

        [JsonIgnore]
        [Required]
        public LiabilityType Type { get; set; }

        [Required]
        public decimal Value { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }
        

        public bool Equals(Liability other)
        {
            if (other is null)
                return false;

            return this.Name == other.Name 
                && this.TypeName == other.TypeName
                && this.Value == other.Value;
        }

        public override bool Equals(object obj) => Equals(obj as Liability);
        public override int GetHashCode() => (Name, TypeName, Value).GetHashCode();
    }

    public class LiabilityDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? UserId { get; set; }
    }
}
