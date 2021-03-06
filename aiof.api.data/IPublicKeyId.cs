﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public interface IPublicKeyId
    {
        [Required]
        [JsonIgnore]
        int Id { get; set; }

        [Required]
        [JsonIgnore]
        Guid PublicKey { get; set; }
    }
}
