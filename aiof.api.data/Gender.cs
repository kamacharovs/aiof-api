﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace aiof.api.data
{
    public class Gender : IGender,
        IPublicKeyName
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid PublicKey { get; set; } = Guid.NewGuid();
    }
}
