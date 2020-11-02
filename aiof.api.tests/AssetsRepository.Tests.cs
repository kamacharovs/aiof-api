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
            decimal value,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAsync(name, typeName, value);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(userId, asset.UserId);
            Assert.False(asset.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_AssetDto_Exists(
            string name,
            string typeName,
            decimal value,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value
            });

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(userId, asset.UserId);
            Assert.False(asset.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUsersId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_Id_Exists(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
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
        [MemberData(nameof(Helper.AssetsTypeNameUserId), MemberType = typeof(Helper))]
        public async Task GetAssetsAsync_By_TypeName_NotEmpty(string typeName, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var assets = await _repo.GetAsync(typeName);

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsUsersId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsSuccessful( int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var assets = await _repo.GetAllAsync();

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Fact]
        public async Task GetAssetTypesAsync_All()
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            var assetTypes = await _repo.GetTypesAsync();

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
            decimal value,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            await Assert.ThrowsAsync<AiofFriendlyException>(() => _repo.AddAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value
            }));
        }

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task AddAssetAsync_Is_Successful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto = Helper.FakerAssetDtos().First();
            var asset = await _repo.AddAsync(dto);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.Equal(dto.Name, asset.Name);
            Assert.Equal(dto.TypeName, asset.TypeName);
            Assert.Equal(dto.Value, asset.Value);
            Assert.Equal(userId, asset.UserId);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUsersId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();

            await _repo.DeleteAsync(id);
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }
    }
}