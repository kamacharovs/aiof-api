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
        Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName);
        Task<Asset> AddAssetAsync(Asset asset);
        IAsyncEnumerable<Asset> AddAssetsAsync(IEnumerable<Asset> assets);
        Task<ILiability> GetLiabilityAsync(int id);
        Task<ILiability> AddLiabilityAsync(Liability liability);
        Task<IGoal> GetGoalAsync(int id);
        Task<IGoal> AddGoalAsync(Goal goal);
        Task<IFinance> GetFinanceAsync(int id);
    }
}