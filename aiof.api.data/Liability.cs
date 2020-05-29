using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Liability : ILiability, IPublicKeyId
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int FinanceId { get; set; }
    }
}
