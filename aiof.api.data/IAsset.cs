using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface IAsset
    {
        int Id { get; set; }
        Guid PublicKey { get; set; }
        string Name { get; set; }
        string Type { get; set; }
    }
}