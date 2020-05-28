using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Liability : ILiability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
