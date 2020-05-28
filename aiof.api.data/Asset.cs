using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Asset : IAsset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Value { get; set; }
    }
}
