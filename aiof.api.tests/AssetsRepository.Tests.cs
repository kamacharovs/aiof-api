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
        public async Task GetAssetAsync_Exists(
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
        }
    }
}