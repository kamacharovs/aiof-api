using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Asset : IAsset, 
        IPublicKeyId, IPublicKeyName
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TypeName { get; set; }
        public AssetType Type { get; set; }
        public decimal Value { get; set; } 
        public int? FinanceId { get; set; }
    }

    public class AssetDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
        public int? FinanceId { get; set; }
    }
}
