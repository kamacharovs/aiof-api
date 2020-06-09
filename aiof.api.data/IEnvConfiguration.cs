using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.Configuration;

namespace aiof.api.data
{
    public interface IEnvConfiguration
    {
        IConfiguration _configuration { get; }
    }
}
