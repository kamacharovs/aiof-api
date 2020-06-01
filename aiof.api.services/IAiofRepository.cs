using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofRepository
    {
        Task<IEnumerable<IAsset>> GetAssets();
        Task<IAsset> GetAssetAsync(int id);
        Task<IFinance> GetFinanceAsync(int id);
    }
}