using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAssetRepository
    {
        Task<IAsset> GetAsync(
            int id,
            bool asNoTracking = true);
        Task<IAsset> GetAsync(
            string name,
            string typeName,
            decimal? value,
            bool asNoTracking = true);
        Task<IAsset> GetAsync(AssetDto assetDto);
        Task<IEnumerable<IAsset>> GetAsync(string typeName);
        Task<IEnumerable<IAsset>> GetAllAsync(bool asNoTracking = true);
        Task<IEnumerable<IAssetType>> GetTypesAsync();
        Task<IAsset> AddAsync(AssetDto assetDto);
        IAsyncEnumerable<IAsset> AddAsync(IEnumerable<AssetDto> assetsDto);
        Task<IAsset> UpdateAsync(
            int id, 
            AssetDto assetDto);
        Task DeleteAsync(int id);
    }
}