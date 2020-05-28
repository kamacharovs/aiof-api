using System;

namespace aiof.api.data
{
    public interface IGoal
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        bool Savings { get; set; }
        string Type { get; set; }
    }
}