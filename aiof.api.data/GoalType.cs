using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class GoalType : IGoalType
    {
        public string Name { get; set; }
        public Guid PublicKey { get; set; }
    }
}
