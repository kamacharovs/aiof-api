using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public class Asset : IAsset, 
        IPublicKeyId, IPublicKeyName
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TypeName { get; set; }
        [JsonIgnore] public AssetType Type { get; set; }
        public decimal Value { get; set; } 
        [JsonIgnore] public int? UserId { get; set; }
    }

    public class AssetDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? UserId { get; set; }
    }
}
