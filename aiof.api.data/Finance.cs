using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Finance : IPublicKeyId
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public Guid UserId { get; set; }

        public List<Asset> Assets { get; set; } = new List<Asset>();
        public List<Liability> Liabilities { get; set; } = new List<Liability>();
        public List<Goal> Goals { get; set; } = new List<Goal>();
    }
}
