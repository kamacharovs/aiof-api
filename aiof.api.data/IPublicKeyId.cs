﻿using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface IPublicKeyId
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
    }
}