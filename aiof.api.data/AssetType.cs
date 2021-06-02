using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class AssetType : IAssetType, 
        IPublicKeyName
    {
        public string Name { get; set; }
        public Guid PublicKey { get; set; }
    }
}
