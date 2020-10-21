using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xunit;

using aiof.api.data;
using aiof.api.services;
using AutoMapper;

namespace aiof.api.tests
{
    public class UserRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ById_IsSuccessful(int id)
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
        public async Task GetUserAsync_ById_NotFound(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id + 1));
        }

        [Theory]
        [MemberData(nameof(Helper.UsersIdUsername), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ByUsername_IsSuccessful(int id, string username)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var user = await _repo.GetAsync(username);

            Assert.Equal(id, user.Id);
            Assert.NotEqual(Guid.Empty, user.PublicKey);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Username);
            Assert.Equal(username, user.Username);
        }
        [Theory]
        [MemberData(nameof(Helper.UsersIdUsername), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ByUsername_NotFound(int id, string username)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(username + "notfound"));
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
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task UpsertAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomUserDto(id);

            Assert.NotNull(dto);

            var user = await _repo.UpsertAsync(id, dto);

            Assert.NotEmpty(user.Assets);
            foreach (var asset in dto.Assets)
            {
                Assert.NotNull(
                    user.Assets.FirstOrDefault(x => x.Name == asset.Name
                        && x.TypeName == asset.TypeName
                        && x.UserId == asset.UserId));
            }

            Assert.NotEmpty(user.Liabilities);
            foreach (var liability in dto.Liabilities)
            {
                Assert.NotNull(
                    user.Liabilities.FirstOrDefault(x => x.Name == liability.Name
                        && x.TypeName == liability.TypeName
                        && x.UserId == liability.UserId));
            }

            Assert.NotEmpty(user.Goals);
            foreach (var goal in dto.Goals)
            {
                Assert.NotNull(
                    user.Goals.FirstOrDefault(x => x.Name == goal.Name
                        && x.TypeName == goal.TypeName
                        && x.UserId == goal.UserId));
            }
        }

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task UpsertProfileAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomUserProfileDto();

            var user = await _repo.UpsertProfileAsync(id, dto);

            Assert.NotNull(user);
            Assert.Equal(user.Profile.Gender, dto.Gender);
            Assert.Equal(user.Profile.DateOfBirth, dto.DateOfBirth);
            Assert.Equal(user.Profile.EducationLevel, dto.EducationLevel);
        }
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task UpsertProfileAsync_NotFound(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomUserProfileDto();

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.UpsertProfileAsync(id + 1, dto));
        }

        [Theory]
        [MemberData(nameof(Helper.SubscriptionsId), MemberType = typeof(Helper))]
        public async Task GetSubscsriptionAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var sub = await _repo.GetSubscriptionAsync(id);

            Assert.NotNull(sub);
            Assert.NotEqual(Guid.Empty, sub.PublicKey);
            Assert.NotNull(sub.Name);
            Assert.NotNull(sub.Description);
            Assert.True(sub.Amount > 0);
            Assert.NotNull(sub.PaymentFrequencyName);
            Assert.NotNull(sub.PaymentFrequency);
            Assert.True(sub.PaymentLength > 0);

            if (!string.IsNullOrEmpty(sub.From))
                Assert.True(sub.From.Length < 200);

            if (!string.IsNullOrEmpty(sub.Url))
                Assert.True(sub.Url.Length < 500);
        }
        [Theory]
        [MemberData(nameof(Helper.SubscriptionsId), MemberType = typeof(Helper))]
        public async Task GetSubscsriptionAsync_ById_IsNull(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var sub = await _repo.GetSubscriptionAsync(id * 5);

            Assert.Null(sub);
        }
        [Theory]
        [MemberData(nameof(Helper.SubscriptionsPublicKey), MemberType = typeof(Helper))]
        public async Task GetSubscsriptionAsync_ByPublicKey_IsNull(int userId, Guid publicKey)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var newPublicKey = publicKey == Guid.Empty ? Guid.NewGuid() : Guid.NewGuid();
            var sub = await _repo.GetSubscriptionAsync(newPublicKey);

            Assert.Null(sub);
        }

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task AddSubscriptionAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomSubscriptionDto(userId);
            var sub = await _repo.AddSubscriptionAsync(dto);

            Assert.NotNull(sub);
            Assert.Equal(userId, sub.UserId);
            Assert.NotEqual(Guid.Empty, sub.PublicKey);
            Assert.NotNull(sub.Name);
            Assert.NotNull(sub.Description);
            Assert.True(sub.Amount > 0);
            Assert.NotNull(sub.PaymentFrequencyName);
            Assert.NotNull(sub.PaymentFrequency);
            Assert.True(sub.PaymentLength > 0);
            Assert.NotNull(sub.From);
            Assert.NotNull(sub.Url);
            Assert.False(sub.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.SubscriptionsId), MemberType = typeof(Helper))]
        public async Task UpdateSubscriptionAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomSubscriptionDto(userId);
            var sub = await _repo.UpdateSubscriptionAsync(id, dto);

            Assert.NotNull(sub);
            Assert.Equal(dto.Amount, sub.Amount);
        }

        [Theory]
        [MemberData(nameof(Helper.SubscriptionsId), MemberType = typeof(Helper))]
        public async Task DeleteSubscriptionAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await _repo.DeleteSubscriptionAsync(id);
            var sub = await _repo.GetSubscriptionAsync(id);

            Assert.Null(sub);
        }

        [Theory]
        [MemberData(nameof(Helper.AccountsId), MemberType = typeof(Helper))]
        public async Task GetAccountAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var acc = await _repo.GetAccountAsync(id);

            Assert.NotNull(acc);
            Assert.Equal(userId, acc.UserId);
            Assert.NotEqual(Guid.Empty, acc.PublicKey);
            Assert.NotNull(acc.Name);
            Assert.NotNull(acc.Description);
            Assert.NotNull(acc.TypeName);
        }
        [Theory]
        [MemberData(nameof(Helper.AccountsId), MemberType = typeof(Helper))]
        public async Task GetAccountAsync_NotFound(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAccountAsync(id * 5));
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

        [Theory]
        [MemberData(nameof(Helper.AccountsId), MemberType = typeof(Helper))]
        public async Task DeleteAccountAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await _repo.DeleteAccountAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAccountAsync(id));
        }
    }
}
