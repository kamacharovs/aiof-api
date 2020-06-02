using System;

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

        [Fact]
        public void Test1()
        {

        }
    }
}
