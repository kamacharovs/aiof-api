﻿using System;

namespace aiof.api.data
{
    public interface ILiability
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        string Type { get; set; }
    }
}