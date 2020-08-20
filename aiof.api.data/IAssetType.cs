using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface IAssetType
    {
        string Name { get; set; }
        Guid PublicKey { get; set; }
    }
}