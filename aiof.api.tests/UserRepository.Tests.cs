using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class UserRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetUserAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var user = await _repo.GetAsync(id);

            Assert.Equal(id, user.Id);
            Assert.NotEqual(Guid.Empty, user.PublicKey);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Username);
        }
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetUserAsync_NotFound(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id + 1));
        }

        [Theory]
        [MemberData(nameof(Helper.UserProfilesId), MemberType = typeof(Helper))]
        public async Task GetUserProfileAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var profile = await _repo.GetProfileAsync(userId);

            Assert.NotNull(profile);
            Assert.NotEqual(profile.PublicKey, Guid.Empty);
            Assert.NotNull(profile.User);
            Assert.NotNull(profile.Gender);
            Assert.NotNull(profile.Occupation);
            Assert.NotNull(profile.OccupationIndustry);
            Assert.NotNull(profile.MaritalStatus);
        }
        [Theory]
        [MemberData(nameof(Helper.UserProfilesId), MemberType = typeof(Helper))]
        public async Task GetUserProfileAsync_NotFound(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetProfileAsync(userId + 1));
        }

        [Theory]
        [MemberData(nameof(Helper.SubscriptionsId), MemberType = typeof(Helper))]
        public async Task GetSubscsriptionAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var subscription = await _repo.GetSubscriptionAsync(id);

            Assert.NotNull(subscription);
            Assert.NotEqual(Guid.Empty, subscription.PublicKey);
            Assert.NotNull(subscription.Name);
            Assert.NotNull(subscription.Description);
            Assert.True(subscription.Amount > 0);
            Assert.NotNull(subscription.PaymentFrequencyName);
            Assert.NotNull(subscription.PaymentFrequency);
            Assert.True(subscription.PaymentLength > 0);

            if (!string.IsNullOrEmpty(subscription.From))
                Assert.True(subscription.From.Length < 200);

            if (!string.IsNullOrEmpty(subscription.Url))
                Assert.True(subscription.Url.Length < 500);
        }

        [Fact]
        public async Task GetAccountTypesAsync_Is_Successful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();
            var accountTypes = await _repo.GetAccountTypesAsync();

            Assert.NotEmpty(accountTypes);
            Assert.NotNull(accountTypes.First().Name);
        }

        [Fact]
        public async Task GetAccountTypeMapsAsync_Is_Successful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();
            var accountTypesMap = await _repo.GetAccountTypeMapsAsync();

            Assert.NotEmpty(accountTypesMap);
            Assert.NotNull(accountTypesMap.First().AccountName);
            Assert.NotNull(accountTypesMap.First().AccountTypeName);
            Assert.NotNull(accountTypesMap.First().AccountType);
            Assert.NotNull(accountTypesMap.First().AccountType.Name);
        }
    }
}
