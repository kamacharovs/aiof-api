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
    }
}
