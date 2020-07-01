using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class BaseRepositoryTests
    {
         private readonly IBaseRepository<Asset> _assetRepo;

        public BaseRepositoryTests()
        {
            _assetRepo = new Helper<IBaseRepository<Asset>>()
                .GetRequiredService() ?? throw new ArgumentNullException(nameof(IBaseRepository<Asset>));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetEntityAsync_Asset_Valid(int id)
        {
            var asset = await _assetRepo.GetEntityAsync(id);

            Assert.NotNull(asset);
            Assert.NotNull(asset.Name);
            Assert.NotNull(asset.TypeName);
        }

        [Theory]
        [InlineData(999)]
        [InlineData(998)]
        [InlineData(997)]
        public async Task GetEntityAsync_Asset_NotFound(int id)
        {
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _assetRepo.GetEntityAsync(id));
        }
    }
}