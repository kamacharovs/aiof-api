using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class AiofRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetUserAsync_By_Id_Exists(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IAiofRepository>();
            var user = await _repo.GetUserAsync(id);

            Assert.NotNull(user);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
        }

        [Theory]
        [MemberData(nameof(Helper.UsersIdEmail), MemberType = typeof(Helper))]
        public async Task GetUserAsync_By_Email_Exists(int id, string email)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IAiofRepository>();
            var user = await _repo.GetUserAsync(email);

            Assert.NotNull(user);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
        }
    }
}
