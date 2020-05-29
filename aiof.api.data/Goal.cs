using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Goal : IGoal, IPublicKeyId
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public GoalType Type { get; set; }
        public bool Savings { get; set; } = false;
        public int FinanceId { get; set; }
    }
}
