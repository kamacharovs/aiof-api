using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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
        [MemberData(nameof(Helper.AssetsNameTypeNameValueFinanceId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_NameTypeNameValueFinanceId_Exists(
            string name,
            string typeName,
            float? value,
            int? financeId)
        {
            var asset = await _repo.GetAssetAsync(
                name,
                typeName,
                value,
                financeId);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(financeId, asset.FinanceId);
        }

        [Theory]
        [MemberData(nameof(Helper.AssetsNameTypeNameValueFinanceId), MemberType = typeof(Helper))]
        public async Task GetAssetAsync_By_AssetDto_Exists(
            string name,
            string typeName,
            float? value,
            int? financeId)
        {
            var asset = await _repo.GetAssetAsync(new AssetDto
            {
                Name = name,
                TypeName = typeName,
                Value = value,
                FinanceId = financeId
            });

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
            Assert.NotNull(asset.Type);
            Assert.Equal(name, asset.Name);
            Assert.Equal(typeName, asset.TypeName);
            Assert.Equal(value, asset.Value);
            Assert.Equal(financeId, asset.FinanceId);
        }
    }
}