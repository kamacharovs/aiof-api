using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

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
