using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAssetRepository
    {
        Task<IAsset> GetAssetAsync(int id);
        Task<IAsset> GetAssetAsync(
            string name,
            string typeName,
            decimal? value,
            int? userId = null);
        Task<IAsset> GetAssetAsync(AssetDto assetDto);
        Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName);
        Task<IEnumerable<IAssetType>> GetAssetTypesAsync();
        Task<IAsset> AddAssetAsync(AssetDto assetDto);
        IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assetsDto);
        Task<IAsset> UpdateAssetAsync(
            int id, 
            AssetDto assetDto);
        Task DeleteAsync(
            string name,
            string typeName,
            decimal value,
            int? userId = null);
        Task DeleteAsync(IAsset asset);
    }
}