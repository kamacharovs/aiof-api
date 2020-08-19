using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class UserRepositoryTests
    {
        private readonly IUserRepository _repo;

        public UserRepositoryTests()
        {
            _repo = Helper.GetRequiredService<IUserRepository>() ?? throw new ArgumentNullException(nameof(IUserRepository));
        }

        [Theory]
        [MemberData(nameof(Helper.UsersUsername), MemberType = typeof(Helper))]
        public async Task GetUserAsync_Valid(string username)
        {
            var user = await _repo.GetUserAsync(username);

            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Username);
        }

        [Theory]
        [MemberData(nameof(Helper.UserProfilesUsername), MemberType = typeof(Helper))]
        public async Task GetUserProfileAsync_Valid(string username)
        {
            var userProfile = await _repo.GetUserProfileAsync(username);

            Assert.NotNull(userProfile);
        }
    }
}
