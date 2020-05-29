using System;
using System.Collections.Generic;
using System.Text;


namespace aiof.api.data
{
    public interface IGoal
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        GoalType Type { get; set; }
        bool Savings { get; set; }
        int FinanceId { get; set; }
    }
}