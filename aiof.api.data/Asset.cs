using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Asset : IAsset, 
        IPublicKeyId, IPublicKeyName, IIsDeleted
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public AssetType Type { get; set; }
        public decimal Value { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
