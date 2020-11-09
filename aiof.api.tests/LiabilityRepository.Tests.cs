using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class LiabilityRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.LiabilitiesIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var liability = await _repo.GetAsync(id);

            Assert.NotNull(liability);
            Assert.NotNull(liability.Name);
            Assert.NotNull(liability.TypeName);
            Assert.NotNull(liability.Type);
            Assert.True(liability.Value > 0);

            if (liability.MonthlyPayment != null)
                Assert.True(liability.MonthlyPayment >= 0);
            if (liability.Years != null)
                Assert.True(liability.Years >= 0);

            Assert.Equal(userId, liability.UserId);
            Assert.False(liability.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_NotFound(int id, int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.GetAsync(id * 100));
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var liabilities = await _repo.GetAllAsync();
            var liability = liabilities.FirstOrDefault();

            Assert.NotNull(liabilities);
            Assert.NotEmpty(liabilities);
            Assert.NotNull(liability);
            Assert.NotNull(liability.Name);
            Assert.NotNull(liability.TypeName);
            Assert.True(liability.Value > 0);
            Assert.Equal(userId, liability.UserId);
            Assert.False(liability.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsEmpty(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId * 100 }.GetRequiredService<ILiabilityRepository>();
            var liabilities = await _repo.GetAllAsync();

            Assert.Empty(liabilities);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesTypeName), MemberType = typeof(Helper))]
        public async Task GetTypeAsync_IsSuccessful(string typeName)
        {
            var _repo = new ServiceHelper().GetRequiredService<ILiabilityRepository>();
            var liabilityType = await _repo.GetTypeAsync(typeName);

            Assert.NotNull(liabilityType);
            Assert.NotNull(liabilityType.Name);
            Assert.NotEqual(Guid.Empty, liabilityType.PublicKey);
        }

        [Fact]
        public async Task GetTypesAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<ILiabilityRepository>();
            var types = await _repo.GetTypesAsync();
            var type = types.FirstOrDefault();

            Assert.NotEmpty(types);
            Assert.NotNull(type);
            Assert.NotNull(type.Name);
            Assert.NotEqual(Guid.Empty, type.PublicKey);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var dto = Helper.RandomLiabilityDto();
            var liability = await _repo.AddAsync(dto);

            Assert.NotNull(liability);
            Assert.NotNull(liability.Name);
            Assert.NotNull(liability.TypeName);
            Assert.True(liability.Value > 0);

            if (liability.MonthlyPayment != null)
                Assert.True(liability.MonthlyPayment >= 0);
            if (liability.Years != null)
                Assert.True(liability.Years >= 0);

            Assert.Equal(userId, liability.UserId);
            Assert.False(liability.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_AlreadyExists_Throws_BadRequest(int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var dto = Helper.RandomLiabilityDto();
            var liability = await repo.AddAsync(dto);

            await Assert.ThrowsAsync<AiofFriendlyException>(() => repo.AddAsync(dto));
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_TypeDoesntExist_Throws_NotFound(int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var dto = Helper.RandomLiabilityDto();
            
            dto.TypeName = "definitelydoesntexist";

            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.AddAsync(dto));
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Multiple_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var dto1 = Helper.RandomLiabilityDto();
            var dto2 = Helper.RandomLiabilityDto();
            var liabilities = _repo.AddAsync(new List<LiabilityDto> { dto1, dto2 });

            await foreach (var liability in liabilities)
            {
                Assert.NotNull(liability);
                Assert.NotNull(liability.Name);
                Assert.NotNull(liability.TypeName);
                Assert.True(liability.Value > 0);

                if (liability.MonthlyPayment != null)
                    Assert.True(liability.MonthlyPayment >= 0);
                if (liability.Years != null)
                    Assert.True(liability.Years >= 0);

                Assert.Equal(userId, liability.UserId);
                Assert.False(liability.IsDeleted);
            }
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            var newValue = 9999M;
            var liability = await _repo.UpdateAsync(
                id,
                new LiabilityDto
                {
                    Value = newValue
                });

            Assert.NotNull(liability);
            Assert.NotNull(liability.Name);
            Assert.NotNull(liability.TypeName);
            Assert.NotNull(liability.Type);
            Assert.True(liability.Value > 0);
            Assert.Equal(newValue, liability.Value);

            if (liability.MonthlyPayment != null)
                Assert.True(liability.MonthlyPayment >= 0);
            if (liability.Years != null)
                Assert.True(liability.Years >= 0);

            Assert.Equal(userId, liability.UserId);
            Assert.False(liability.IsDeleted);
        }

        [Theory]
        [InlineData("type-number-1")]
        [InlineData("type-number-2")]
        [InlineData("type-number-3")]
        public async Task AddTypeAsync_IsSuccessful(string name)
        {
            var _repo = new ServiceHelper().GetRequiredService<ILiabilityRepository>();
            var type = await _repo.AddTypeAsync(name);

            Assert.NotNull(type);
            Assert.Equal(name, type.Name);
            Assert.NotEqual(Guid.Empty, type.PublicKey);
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
            await _repo.DeleteAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.LiabilitiesIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_NotFound(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<ILiabilityRepository>();
          
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.DeleteAsync(id * 100));
        }
    }
}