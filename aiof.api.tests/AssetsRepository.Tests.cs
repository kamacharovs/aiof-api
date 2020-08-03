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
        private readonly IAssetRepository _repo;

        public AssetsRepositoryTests()
        {
            _repo = Helper.GetRequiredService<IAssetRepository>() ?? throw new ArgumentNullException(nameof(IAssetRepository));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_NameTypeNameValueUserId_Exists(
            string name,
            string typeName,
            decimal? value,
            int? userId)
        {
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
            var asset = await _repo.GetAssetAsync(id);

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
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAssetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsTypeName), MemberType = typeof(Helper))]
        public async Task GetAssetsAsync_By_TypName_NotEmpty(string typeName)
        {
            var assets = await _repo.GetAssetsAsync(typeName);

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Fact]
        public async Task GetAssetTypesAsync_All()
        {
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
    }
}