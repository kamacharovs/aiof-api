using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Goal : IGoal, 
        IPublicKeyId, IPublicKeyName, IEntity
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string TypeName { get; set; }
        public GoalType Type { get; set; }
        public bool Savings { get; set; } = false;
        public int? FinanceId { get; set; }
    }

    public class GoalDto
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public bool? Savings { get; set; }
        public int? FinanceId { get; set; }
    }
}
