using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace aiof.api.data
{
    public interface ITenant
    {
        int UserId { get; set; }
        int ClientId { get; set; }
        Guid PublicKey { get; set; }
    }
}
