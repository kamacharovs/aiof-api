using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class AiofRepositoryTests
    {
        private readonly IAiofRepository _repo;

        public AiofRepositoryTests()
        {
            _repo = Helper.GetRequiredService<IAiofRepository>() ?? throw new ArgumentNullException(nameof(IAiofRepository));
        }

        [Theory]
        [InlineData(1)]
        public async Task GetUserAsync_Valid(int id)
        {
            var user = await _repo.GetUserAsync(id);

            Assert.NotNull(user);
            Assert.Equal("Georgi", user.FirstName);
        }

        /*
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAssetAsync_Exists(int id)
        {
            var asset = await _repo.GetAssetAsync(id);

            Assert.NotNull(asset);
            Assert.NotEqual(Guid.Empty, asset.PublicKey);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.NotEqual(0, asset.Value);
            Assert.NotEqual(0, asset.FinanceId);
        }

        [Theory]
        [InlineData("car")]
        [InlineData("house")]
        public async  Task GetAssetsAsync_GetBy_OrderBy_TypeName_NotEmpty(string typeName)
        {
            var assets = await _repo.GetAssetsAsync(typeName);

            Assert.NotNull(assets);
            Assert.NotEmpty(assets);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test 1")]
        [InlineData("doesn't exist")]
        public async Task GetAssetsAsync_GetBy_OrderBy_TypeName_Empty(string typeName)
        {
            var assets = await _repo.GetAssetsAsync(typeName);

            Assert.Empty(assets);
        }

        [Theory]
        [InlineData("car", "car", 3000, 1)]
        [InlineData("house", "house", 600000, 1)]
        [InlineData("cash", "cash", 15000, 1)]
        [InlineData("other", "other", 300, 1)]
        public async Task AddAssetAsync_Valid(string name, string typeName, double value, int financeId)
        {
            var asset = await _repo.AddAssetAsync(new AssetDto()
                {
                    Name = name,
                    TypeName = typeName,
                    Value = (float)value,
                    FinanceId = financeId
                });

            Assert.NotNull(asset);
            Assert.NotNull(asset?.Name);
            Assert.NotNull(asset?.TypeName);
        }

        [Theory]
        [InlineData("car", "house")]
        public async Task AddAssetsAsync_MultipleAssets(string typeName1, string typeName2)
        {
            var typeNames = new List<string>() { typeName1, typeName2 };
            var asset1 = new AssetDto()
            {
                Name = typeName1,
                TypeName = typeName1,
                Value = 1.0F,
                FinanceId = 1
            };

            var asset2 = new AssetDto()
            {
                Name = typeName2,
                TypeName = typeName2,
                Value = 1.0F,
                FinanceId = 1
            };

            var assets = _repo.AddAssetsAsync(new List<AssetDto>() { asset1, asset2 });

            await foreach (var asset in assets)
            {
                Assert.NotNull(asset);
                Assert.Contains(asset.TypeName, typeNames);
            }
        }
        */

        [Theory]
        [InlineData(1, 1)]
        public async Task GetFinanceAsync_Exists(int id, int userId)
        {
            var finance = await _repo.GetFinanceAsync(id, userId);

            Assert.NotNull(finance);
            Assert.NotEmpty(finance.Assets);
            Assert.NotEmpty(finance.Liabilities);
        }
    }
}
