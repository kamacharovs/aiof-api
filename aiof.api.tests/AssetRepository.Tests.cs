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
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var asset = await _repo.GetAsync(id);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_NotFound(int id, int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.GetAsync(id * 100));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_InvalidUserId_NotFound(int id, int userId)
        {
            var repo = new ServiceHelper() { UserId = userId * 100 }.GetRequiredService<IAssetRepository>();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.GetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_By_NameTypeNameValueUserId_IsSuccessful(
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
        public async Task GetAsync_By_AssetDto_IsSuccessful(
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
        [MemberData(nameof(Helper.AssetsTypeNameUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ByTypeName_IsSuccessful(string typeName, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var assets = await _repo.GetAsync(typeName);

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsUserId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsSuccessful( int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var assets = await _repo.GetAllAsync();

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Fact]
        public async Task GetTypesAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IAssetRepository>();
            var assetTypes = await _repo.GetTypesAsync();
            var assetType = assetTypes.FirstOrDefault();

            Assert.NotNull(assetTypes);
            Assert.NotEmpty(assetTypes);
            Assert.NotNull(assetType);
            Assert.NotNull(assetType.Name);
            Assert.NotEqual(Guid.Empty, assetType.PublicKey);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_AlreadyExists_Throws_AiofFriendlyException(
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
        [MemberData(nameof(Helper.AssetsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto = Helper.RandomAssetDto();
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
        [MemberData(nameof(Helper.AssetsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Multiple_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto1 = Helper.RandomAssetDto();
            var dto2 = Helper.RandomAssetDto();
            var assets = _repo.AddAsync(new List<AssetDto> { dto1, dto2 });

            await foreach (var asset in assets)
            {
                Assert.NotNull(asset);
                Assert.NotNull(asset.Name);
                Assert.NotNull(asset.TypeName);
                Assert.Equal(userId, asset.UserId);
            }
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_TypeDoesntExist_Throws_NotFound(int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto = Helper.RandomAssetDto();
            
            dto.TypeName = "definitelydoesntexist";

            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.AddAsync(dto));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto = Helper.RandomAssetDto();
            var asset = await _repo.UpdateAsync(id, dto);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.Equal(dto.Name, asset.Name);
            Assert.Equal(dto.TypeName, asset.TypeName);
            Assert.Equal(dto.Value, asset.Value);
            Assert.Equal(userId, asset.UserId);
        }  

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_DtoIsNull_Throws_BadRequest(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            
            await Assert.ThrowsAsync<AiofFriendlyException>(() => _repo.UpdateAsync(id, null));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_DoesntExist_Throws_NotFound(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            var dto = Helper.RandomAssetDto();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.UpdateAsync(id * 100, dto));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
            await _repo.DeleteAsync(id);
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_NotFound(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IAssetRepository>();
          
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.DeleteAsync(id * 100));
        }
    }
}