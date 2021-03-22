﻿using System;
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
        public async Task GetUserAsync_ById_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var user = await _repo.GetAsync();

            Assert.Equal(id, user.Id);
            Assert.NotEqual(Guid.Empty, user.PublicKey);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
            Assert.True(user.Dependents.Count() >= 0);
            Assert.True(user.Assets.Count() >= 0);
            Assert.True(user.Liabilities.Count() >= 0);
            Assert.True(user.Goals.Count() >= 0);
            Assert.True(user.Subscriptions.Count() >= 0);
            Assert.True(user.Accounts.Count() >= 0);
        }
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ById_NotFound(int id)
        {
            var _repo = new ServiceHelper() { UserId = id * 5 }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync());
        }

        [Theory]
        [MemberData(nameof(Helper.UserProfilesIdEmail), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ByEmail_IsSuccessful(int id, string email)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var user = await _repo.GetAsync(email);

            Assert.Equal(id, user.Id);
            Assert.NotEqual(Guid.Empty, user.PublicKey);
            Assert.NotNull(user.FirstName);
            Assert.NotNull(user.LastName);
            Assert.NotNull(user.Email);
            Assert.Equal(email, user.Email);
            Assert.True(user.Dependents.Count() >= 0);
            Assert.True(user.Assets.Count() >= 0);
            Assert.True(user.Liabilities.Count() >= 0);
            Assert.True(user.Goals.Count() >= 0);
            Assert.True(user.Subscriptions.Count() >= 0);
            Assert.True(user.Accounts.Count() >= 0);
        }
        [Theory]
        [MemberData(nameof(Helper.UsersIdEmail), MemberType = typeof(Helper))]
        public async Task GetUserAsync_ByEmail_NotFound(int id, string email)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(email + "notfound"));
        }

        [Theory]
        [MemberData(nameof(Helper.UserProfilesId), MemberType = typeof(Helper))]
        public async Task GetUserProfileAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var profile = await _repo.GetProfileAsync();

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
            var _repo = new ServiceHelper() { UserId = userId * 5 }.GetRequiredService<IUserRepository>();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() =>_repo.GetProfileAsync());
        }

        /*
        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task UpsertAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var rdto = Helper.RandomUserDtos().FirstOrDefault().ToArray();
            var dto = new UserDto
            {
                Assets = (ICollection<AssetDto>)rdto[0],
                Liabilities = (ICollection<LiabilityDto>)rdto[1],
                Goals = (ICollection<GoalDto>)rdto[2]
            };

            Assert.NotNull(dto);

            var user = await _repo.UpsertAsync(dto);

            Assert.NotEmpty(user.Assets);
            foreach (var asset in dto.Assets)
            {
                Assert.NotNull(
                    user.Assets.FirstOrDefault(x => x.Name == asset.Name
                        && x.TypeName == asset.TypeName));
            }

            Assert.NotEmpty(user.Liabilities);
            foreach (var liability in dto.Liabilities)
            {
                Assert.NotNull(
                    user.Liabilities.FirstOrDefault(x => x.Name == liability.Name
                        && x.TypeName == liability.TypeName));
            }

            Assert.NotEmpty(user.Goals);
            foreach (var goal in dto.Goals)
            {
                Assert.NotNull(
                    user.Goals.FirstOrDefault(x => x.Name == goal.Name
                        && x.TypeName == goal.TypeName));
            }
        }*/

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task UpsertProfileAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomUserProfileDto();

            var profile = await _repo.UpsertProfileAsync(dto);

            Assert.NotNull(profile);
            Assert.Equal(profile.Gender, dto.Gender);
            Assert.Equal(profile.DateOfBirth, dto.DateOfBirth);
            Assert.Equal(profile.EducationLevel, dto.EducationLevel);
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task GetDependent_ById_IsSuccessful(
            int id,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            var dependent = await _repo.GetDependentAsync(id);

            Assert.NotNull(dependent);
            Assert.NotEqual(0, dependent.Id);
            Assert.NotEqual(Guid.Empty, dependent.PublicKey);
            Assert.NotNull(dependent.FirstName);
            Assert.NotNull(dependent.LastName);
            Assert.NotEqual(0, dependent.Age);
            Assert.NotEqual(0, dependent.AmountOfSupportProvided);
            Assert.NotNull(dependent.UserRelationship);
            Assert.NotEqual(0, dependent.UserId);
            Assert.NotEqual(DateTime.MinValue, dependent.Created);
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task GetDependentAsync_ById_NotFound(
            int id,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetDependentAsync(id * 100));
        }

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task GetDependentAsync_ByDto_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            var dependentDto = new UserDependentDto
            {
                FirstName = $"FirstName{userId}",
                LastName = $"Lastname{userId}",
                Age = 10,
                Email = null,
                AmountOfSupportProvided = 15000M,
                UserRelationship = UserRelationship.Child.ToString()
            };
            await _repo.AddDependentAsync(dependentDto);
            var dependent = await _repo.GetDependentAsync(dependentDto);

            Assert.NotNull(dependent);
            Assert.NotEqual(0, dependent.Id);
            Assert.NotEqual(Guid.Empty, dependent.PublicKey);
            Assert.NotNull(dependent.FirstName);
            Assert.NotNull(dependent.LastName);
            Assert.NotEqual(0, dependent.Age);
            Assert.NotEqual(0, dependent.AmountOfSupportProvided);
            Assert.NotNull(dependent.UserRelationship);
            Assert.NotEqual(0, dependent.UserId);
            Assert.NotEqual(DateTime.MinValue, dependent.Created);
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdWithDependents), MemberType = typeof(Helper))]
        public async Task GetDependentsAsync_IsSuccessful(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();

            var dependents = await _repo.GetDependentsAsync();
            var dependent = dependents.FirstOrDefault();

            Assert.NotNull(dependents);
            Assert.NotEmpty(dependents);
            Assert.NotNull(dependent);
            Assert.NotEqual(0, dependent.Id);
            Assert.NotEqual(Guid.Empty, dependent.PublicKey);
            Assert.NotNull(dependent.FirstName);
            Assert.NotNull(dependent.LastName);
            Assert.NotEqual(0, dependent.Age);
            Assert.NotEqual(0, dependent.AmountOfSupportProvided);
            Assert.NotNull(dependent.UserRelationship);
            Assert.NotEqual(0, dependent.UserId);
            Assert.NotEqual(DateTime.MinValue, dependent.Created);
        }

        [Theory]
        [InlineData(777)]
        [InlineData(888)]
        [InlineData(999)]
        public async Task GetDependentsAsync_IsEmpty(int id)
        {
            var _repo = new ServiceHelper() { UserId = id }.GetRequiredService<IUserRepository>();

            var dependents = await _repo.GetDependentsAsync();
            var dependent = dependents.FirstOrDefault();

            Assert.Empty(dependents);
            Assert.Null(dependent);
        }

        [Theory]
        [MemberData(nameof(Helper.UsersId), MemberType = typeof(Helper))]
        public async Task AddDependentAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            var dependent = await _repo.AddDependentAsync(
                new UserDependentDto
                {
                    FirstName = $"FirstName{userId}",
                    LastName = $"Lastname{userId}",
                    Age = 10,
                    Email = null,
                    AmountOfSupportProvided = 15000M,
                    UserRelationship = UserRelationship.Child.ToString()
                });

            Assert.NotNull(dependent);
            Assert.NotEqual(0, dependent.Id);
            Assert.NotEqual(Guid.Empty, dependent.PublicKey);
            Assert.NotNull(dependent.FirstName);
            Assert.NotNull(dependent.LastName);
            Assert.NotEqual(0, dependent.Age);
            Assert.NotEqual(0, dependent.AmountOfSupportProvided);
            Assert.NotNull(dependent.UserRelationship);
            Assert.NotEqual(0, dependent.UserId);
            Assert.NotEqual(DateTime.MinValue, dependent.Created);
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task UpdateDependentAsync_IsSuccessful(
            int userId,
            int userDependentId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            var dependentDto = new UserDependentDto
            {
                LastName = "Smith"
            };

            var dependent = await _repo.UpdateDependentAsync(userDependentId, dependentDto);

            Assert.NotNull(dependent);
            Assert.Equal(dependentDto.LastName, dependent.LastName);
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task UpdateDependentAsync_NotFound(
            int userId,
            int userDependentId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await Assert.ThrowsAsync<AiofNotFoundException>(() => 
            _repo.UpdateDependentAsync(
                userDependentId * 100, new UserDependentDto
                {
                    LastName = "Smith"
                }));
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task DeleteDependentAsync_ById_IsSuccessful(
            int id,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await _repo.DeleteDependentAsync(id);

            await Assert.ThrowsAnyAsync<AiofNotFoundException>(() => _repo.GetDependentAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.UserIdUserDependentId), MemberType = typeof(Helper))]
        public async Task DeleteDependentAsync_ById_NotFound(
            int id,
            int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await Assert.ThrowsAnyAsync<AiofNotFoundException>(() => _repo.DeleteDependentAsync(id * 100));
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

            await Assert.ThrowsAnyAsync<AiofNotFoundException>(() => _repo.GetSubscriptionAsync(id * 100));
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
            var dto = Helper.RandomSubscriptionDto();
            var sub = await _repo.AddSubscriptionAsync(dto);

            Assert.NotNull(sub);
            Assert.Equal(userId, sub.UserId);
            Assert.NotEqual(Guid.Empty, sub.PublicKey);
            Assert.NotNull(sub.Name);
            Assert.NotNull(sub.Description);
            Assert.True(sub.Amount > 0);
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
            var dto = Helper.RandomSubscriptionDto();
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

            await Assert.ThrowsAnyAsync<AiofNotFoundException>(() => _repo.GetSubscriptionAsync(id));
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

        [Theory]
        [MemberData(nameof(Helper.AccountsUserId), MemberType = typeof(Helper))]
        public async Task AddAccountAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomAccountDto();
            var account = await _repo.AddAccountAsync(dto);

            Assert.NotNull(account);
            Assert.NotNull(account.Name);
            Assert.NotNull(account.Description);
            Assert.NotNull(account.TypeName);
            Assert.False(account.IsDeleted);
            Assert.Equal(dto.Name, account.Name);
            Assert.Equal(dto.Description, account.Description);
            Assert.Equal(dto.TypeName, account.TypeName);
        }

        [Theory]
        [MemberData(nameof(Helper.AccountsUserId), MemberType = typeof(Helper))]
        public async Task AddAccountAsync_AlreadyExists(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomAccountDto();
            var account = await _repo.AddAccountAsync(dto);

            await Assert.ThrowsAsync<AiofFriendlyException>(() => _repo.AddAccountAsync(dto));
        }

        [Theory]
        [MemberData(nameof(Helper.AccountsId), MemberType = typeof(Helper))]
        public async Task UpdateAccountAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();
            var dto = Helper.RandomAccountDto();
            var account = await _repo.UpdateAccountAsync(id, dto);

            Assert.NotNull(account);
            Assert.NotNull(account.Name);
            Assert.NotNull(account.Description);
            Assert.NotNull(account.TypeName);
            Assert.False(account.IsDeleted);
            Assert.Equal(dto.Name, account.Name);
            Assert.Equal(dto.Description, account.Description);
            Assert.Equal(dto.TypeName, account.TypeName);
        }

        [Theory]
        [MemberData(nameof(Helper.AccountsId), MemberType = typeof(Helper))]
        public async Task DeleteAccountAsync_IsSuccessful(int userId, int id)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IUserRepository>();

            await _repo.DeleteAccountAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAccountAsync(id));
        }

        [Fact]
        public async Task GetEducationLevelsAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var educationLevels = await _repo.GetEducationLevelsAsync();
            var educationLevel = educationLevels.FirstOrDefault();

            Assert.NotNull(educationLevels);
            Assert.NotEmpty(educationLevels);
            Assert.NotNull(educationLevel);
            Assert.NotNull(educationLevel.Name);
            Assert.NotEqual(Guid.Empty, educationLevel.PublicKey);
        }

        [Fact]
        public async Task GetMaritalStatusesAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var maritalStatuses = await _repo.GetMaritalStatusesAsync();
            var maritalStatus = maritalStatuses.FirstOrDefault();

            Assert.NotNull(maritalStatuses);
            Assert.NotEmpty(maritalStatuses);
            Assert.NotNull(maritalStatus);
            Assert.NotNull(maritalStatus.Name);
            Assert.NotEqual(Guid.Empty, maritalStatus.PublicKey);
        }

        [Fact]
        public async Task GetResidentialStatusesAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var residentialStatuses = await _repo.GetResidentialStatusesAsync();
            var residentialStatus = residentialStatuses.FirstOrDefault();

            Assert.NotNull(residentialStatuses);
            Assert.NotEmpty(residentialStatuses);
            Assert.NotNull(residentialStatus);
            Assert.NotNull(residentialStatus.Name);
            Assert.NotEqual(Guid.Empty, residentialStatus.PublicKey);
        }

        [Fact]
        public async Task GetGendersAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var genders = await _repo.GetGendersAsync();
            var gender = genders.FirstOrDefault();

            Assert.NotNull(genders);
            Assert.NotEmpty(genders);
            Assert.NotNull(gender);
            Assert.NotNull(gender.Name);
            Assert.NotEqual(Guid.Empty, gender.PublicKey);
        }

        [Fact]
        public async Task GetHouseholdAdultsAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var hhAdults = await _repo.GetHouseholdAdultsAsync();
            var hhAdult = hhAdults.FirstOrDefault();

            Assert.NotNull(hhAdults);
            Assert.NotEmpty(hhAdults);
            Assert.NotNull(hhAdult);
            Assert.NotNull(hhAdult.Name);
            Assert.True(hhAdult.Value >= 0);
            Assert.NotEqual(Guid.Empty, hhAdult.PublicKey);
        }

        [Fact]
        public async Task GetHouseholdChildrenAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var hhChildren = await _repo.GetHouseholdChildrenAsync();
            var hhChild = hhChildren.FirstOrDefault();

            Assert.NotNull(hhChildren);
            Assert.NotEmpty(hhChildren);
            Assert.NotNull(hhChild);
            Assert.NotNull(hhChild.Name);
            Assert.True(hhChild.Value >= 0);
            Assert.NotEqual(Guid.Empty, hhChild.PublicKey);
        }

        [Fact]
        public async Task GetUserProfileOptionsAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IUserRepository>();

            var userProfileOptions = await _repo.GetUserProfileOptionsAsync();

            Assert.NotNull(userProfileOptions.EducationLevels);
            Assert.NotNull(userProfileOptions.MaritalStatuses);
            Assert.NotNull(userProfileOptions.ResidentialStatuses);
            Assert.NotNull(userProfileOptions.Genders);
            Assert.NotNull(userProfileOptions.HouseholdAdults);
            Assert.NotNull(userProfileOptions.HouseholdChildren);
            Assert.NotEmpty(userProfileOptions.EducationLevels);
            Assert.NotEmpty(userProfileOptions.MaritalStatuses);
            Assert.NotEmpty(userProfileOptions.ResidentialStatuses);
            Assert.NotEmpty(userProfileOptions.Genders);
            Assert.NotEmpty(userProfileOptions.HouseholdAdults);
            Assert.NotEmpty(userProfileOptions.HouseholdChildren);

            AssertIPublicKeyName(userProfileOptions.EducationLevels.FirstOrDefault() as IPublicKeyName);
            AssertIPublicKeyName(userProfileOptions.MaritalStatuses.FirstOrDefault() as IPublicKeyName);
            AssertIPublicKeyName(userProfileOptions.ResidentialStatuses.FirstOrDefault() as IPublicKeyName);
            AssertIPublicKeyName(userProfileOptions.Genders.FirstOrDefault() as IPublicKeyName);
            AssertIPublicKeyName(userProfileOptions.HouseholdAdults.FirstOrDefault() as IPublicKeyName);
            AssertIPublicKeyName(userProfileOptions.HouseholdChildren.FirstOrDefault() as IPublicKeyName);
        }

        private void AssertIPublicKeyName(IPublicKeyName obj)
        {
            Assert.NotNull(obj);
            Assert.NotNull(obj.Name);
            Assert.NotEqual(Guid.Empty, obj.PublicKey);
        }
    }
}
