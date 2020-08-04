using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public class Liability : IEquatable<Liability>, ILiability, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TypeName { get; set; }
        [JsonIgnore] public LiabilityType Type { get; set; }
        public decimal Value { get; set; }
        [JsonIgnore] public int? UserId { get; set; }

        public bool Equals(Liability other)
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

    public class LiabilityDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? UserId { get; set; }
    }
}
