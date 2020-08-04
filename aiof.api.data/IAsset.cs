﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aiof.api.data
{
    public interface IAsset
    {
        [JsonIgnore] int Id { get; set; }
        [JsonIgnore] Guid PublicKey { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        [JsonIgnore] AssetType Type { get; set; }
        decimal Value { get; set; }
        [JsonIgnore] int? UserId { get; set; }
    }
}