using System;
using System.Threading.Tasks;

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
            _repo = new Helper<IAiofRepository>()
                .GetRequiredService() ?? throw new ArgumentNullException(nameof(IAiofRepository));
        }

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
        [InlineData(1)]
        public async Task GetFinanceAsync_Exists(int id)
        {
            var finance = await _repo.GetFinanceAsync(id);

            Assert.NotNull(finance);
            Assert.NotEmpty(finance.Assets);
            Assert.NotEmpty(finance.Liabilities);
            Assert.NotEmpty(finance.Goals);
        }
    }
}
