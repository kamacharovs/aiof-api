using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public interface ITenant
    {
        int? UserId { get; set; }
        int? ClientId { get; set; }
    }
}
