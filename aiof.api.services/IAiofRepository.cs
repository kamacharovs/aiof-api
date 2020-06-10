using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofRepository
    {
        Task<User> GetUserAsync(int id);
        Task<IAsset> GetAssetAsync(int id);
        Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName);
        Task<IAsset> AddAssetAsync(AssetDto asset);
        IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assets);
        Task<ILiability> GetLiabilityAsync(int id);
        Task<Liability> AddLiabilityAsync(Liability liability);
        IAsyncEnumerable<Liability> AddLiabilitiesAsync(IEnumerable<Liability> liabilities);
        Task<IGoal> GetGoalAsync(int id);
        Task<Goal> AddGoalAsync(Goal goal);
        IAsyncEnumerable<Goal> AddGoalsAsync(IEnumerable<Goal> goals);
        Task<IFinance> GetFinanceAsync(int id);
        Task<IFinance> AddFinanceAsync(int userId,
            IEnumerable<AssetDto> assets,
            IEnumerable<Liability> liabilities,
            IEnumerable<Goal> goals);
    }
}