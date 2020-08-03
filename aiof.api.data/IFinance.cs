using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface IFinance
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        int UserId { get; set; }
        User User { get; set; }
        List<Asset> Assets { get; set; }
        List<Liability> Liabilities { get; set; }
    }
}