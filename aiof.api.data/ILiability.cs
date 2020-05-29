using System;
using System.Collections.Generic;
using System.Text;


namespace aiof.api.data
{
    public interface ILiability
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        string TypeName { get; set; }
        LiabilityType Type { get; set; }
        float Value { get; set; }
        int FinanceId { get; set; }
    }
}