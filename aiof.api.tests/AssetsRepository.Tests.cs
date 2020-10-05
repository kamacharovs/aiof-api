using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class AssetsRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_NameTypeNameValueUserId_Exists(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAssetAsync(
                name,
                typeName,
                value,
                userId);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(userId, asset.UserId);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_AssetDto_Exists(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAssetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value,
                UserId = userId
            });

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(userId, asset.UserId);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_Id_Exists(int id)
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAsync(id);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
        }

        [Theory]
        [InlineData(7777)]
        [InlineData(8888)]
        [InlineData(9999)]
        public async Task GetAssetAsync_By_Id_DoesntExist_Throws_AiofNotFoundException(int id)
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsTypeName), MemberType = typeof(Helper))]
        public async Task GetAssetsAsync_By_TypName_NotEmpty(string typeName)
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            var assets = await _repo.GetAssetsAsync(typeName);

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Fact]
        public async Task GetAssetTypesAsync_All()
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            var assetTypes = await _repo.GetAssetTypesAsync();

            Assert.NotNull(assetTypes);
            Assert.NotEmpty(assetTypes);
            Assert.NotNull(assetTypes.First().Name);
            Assert.NotEqual(Guid.Empty, assetTypes.First().PublicKey);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task AddAssetAsync_AlreadyExists_Throws_AiofFriendlyException(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            await Assert.ThrowsAsync<AiofFriendlyException>(() => _repo.AddAssetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value,
                UserId = userId
            }));
        }

        [Theory]
        [MemberData(nameof(Helper.RandomAssetDtos), MemberType = typeof(Helper))]
        public async Task AddAssetAsync_Is_Successful(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.AddAssetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value,
                UserId = userId
            });

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(userId, asset.UserId);
        }

        [Theory]
        [MemberData(nameof(Helper.RandomAssetDtos), MemberType = typeof(Helper))]
        public async Task DeleteAsync_By_Asset_Is_Successful(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.AddAssetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value,
                UserId = userId
            });
            Assert.NotNull(await _repo.GetAssetAsync(asset.Name, asset.TypeName, asset.Value));

            await _repo.DeleteAsync(asset.Id);
            Assert.Null(await _repo.GetAssetAsync(asset.Name, asset.TypeName, asset.Value));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_Existing_Is_Successful(int id)
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            await _repo.DeleteAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }
    }
}