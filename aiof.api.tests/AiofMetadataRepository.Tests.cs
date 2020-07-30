using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class AiofMetadataRepositoryTests
    {
        private readonly IAiofMetadataRepository _repo;

        public AiofMetadataRepositoryTests()
        {
            _repo = Helper.GetRequiredService<IAiofMetadataRepository>() ?? throw new ArgumentNullException(nameof(IAiofMetadataRepository));
        }

        [Fact]
        public async Task GetFrequencies_Valid()
        {
            var frequencies = await _repo.GetFrequenciesAsync();

            Assert.NotEmpty(frequencies);
        }
    }
}