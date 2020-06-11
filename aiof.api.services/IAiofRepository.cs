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
        Task<IEnumerable<IAssetType>> GetAssetTypesAsync();
        Task<IAsset> AddAssetAsync(AssetDto asset);
        IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assets);
        Task<ILiability> GetLiabilityAsync(int id);
        Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync();
        Task<ILiability> AddLiabilityAsync(LiabilityDto liability);
        IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilities);
        Task<IGoal> GetGoalAsync(int id);
        Task<IEnumerable<IGoalType>> GetGoalTypesAsync();
        Task<IGoal> AddGoalAsync(GoalDto goal);
        IAsyncEnumerable<IGoal> AddGoalsAsync(IEnumerable<GoalDto> goals);
        Task<IGoal> UpdateGoalAsync(int id, GoalDto goalDto);
        Task<IFinance> GetFinanceAsync(int id);
        Task<IFinance> AddFinanceAsync(FinanceDto financeDto);
    }
}