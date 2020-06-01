using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class LiabilityType : ILiabilityType, IPublicKeyName
    {
        public string Name { get; set; }
        public Guid PublicKey { get; set; }
    }
}
