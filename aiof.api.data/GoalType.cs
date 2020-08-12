﻿using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class GoalType : IGoalType, 
        IPublicKeyName
    {
        public string Name { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
    }
}
