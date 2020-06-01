using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aiof.api.services
{
    public interface IAiofMetadataRepository
    {
        Task SendMetadataAsync();
    }
}
