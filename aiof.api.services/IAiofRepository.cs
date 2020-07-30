using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofRepository
    {
        Task<IUser> GetUserAsync(int id);
        Task<IUser> GetUserAsync(string username);
        Task<IUser> AddUserAsync(UserDto userDto);
        Task<bool> IsUserUniqueAsync(string username, string email);
        Task<IAsset> GetAssetAsync(int id);
        Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName);
        Task<IEnumerable<IAssetType>> GetAssetTypesAsync();
        Task<IAsset> AddAssetAsync(AssetDto asset);
        IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assets);
        Task<IAsset> UpdateAssetAsync(int id, AssetDto assetDto);
        Task<ILiability> GetLiabilityAsync(int id);
        Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync();
        Task<ILiability> AddLiabilityAsync(LiabilityDto liability);
        IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilities);
        Task<ILiability> UpdateLiabilityAsync(int id, LiabilityDto liabilityDto);
        Task<IFinance> GetFinanceAsync(int id, int userId);
        Task<IFinance> AddFinanceAsync(FinanceDto financeDto);
    }
}