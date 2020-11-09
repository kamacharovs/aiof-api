using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class GoalRepositoryTests
    {
        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var goal = await _repo.GetAsync(id);

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.NotNull(goal.TypeName);
            Assert.NotNull(goal.Type);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.Contribution > 0);
            Assert.NotNull(goal.ContributionFrequencyName);
            Assert.NotNull(goal.ContributionFrequency);
            Assert.Equal(userId, goal.UserId);
            Assert.False(goal.IsDeleted);

            if (goal.PlannedDate != null)
                Assert.NotEqual(new DateTime(), goal.PlannedDate);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsTypeName), MemberType = typeof(Helper))]
        public async Task GetAsync_ByTypeName_IsSuccessful(string typeName)
        {
            var _repo = new ServiceHelper().GetRequiredService<IGoalRepository>();
            var goals = await _repo.GetAsync(typeName);
            var goal = goals.FirstOrDefault();

            Assert.NotEmpty(goals);
            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.NotNull(goal.TypeName);
            Assert.NotNull(goal.Type);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.Contribution > 0);
            Assert.NotNull(goal.ContributionFrequencyName);
            Assert.NotNull(goal.ContributionFrequency);
            Assert.False(goal.IsDeleted);

            if (goal.PlannedDate != null)
                Assert.NotEqual(new DateTime(), goal.PlannedDate);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var goals = await _repo.GetAllAsync();
            var goal = goals.FirstOrDefault();

            Assert.NotEmpty(goals);
            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.NotNull(goal.TypeName);
            Assert.NotNull(goal.Type);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.Contribution > 0);
            Assert.NotNull(goal.ContributionFrequencyName);
            Assert.NotNull(goal.ContributionFrequency);
            Assert.False(goal.IsDeleted);

            if (goal.PlannedDate != null)
                Assert.NotEqual(new DateTime(), goal.PlannedDate);
        }

        [Fact]
        public async Task GetTypesAsync_IsSuccessful()
        {
            var _repo = new ServiceHelper().GetRequiredService<IGoalRepository>();
            var types = await _repo.GetTypesAsync();
            var type = types.FirstOrDefault();

            Assert.NotEmpty(types);
            Assert.NotNull(type);
            Assert.NotNull(type.Name);
            Assert.NotEqual(Guid.Empty, type.PublicKey);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = Helper.RandomGoalDto();
            var goal = await _repo.AddAsync(dto);

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.NotNull(goal.TypeName);
            Assert.NotNull(goal.Type);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.Contribution > 0);
            Assert.NotNull(goal.ContributionFrequencyName);
            Assert.NotNull(goal.ContributionFrequency);
            Assert.False(goal.IsDeleted);

            if (goal.PlannedDate != null)
                Assert.NotEqual(new DateTime(), goal.PlannedDate);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Multiple_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto1 = Helper.RandomGoalDto();
            var dto2 = Helper.RandomGoalDto();
            var goals = _repo.AddAsync(new List<GoalDto> { dto1, dto2 });

            await foreach (var goal in goals)
            {
                Assert.NotNull(goal);
                Assert.NotNull(goal.Name);
                Assert.NotNull(goal.TypeName);
                Assert.NotNull(goal.Type);
                Assert.True(goal.Amount > 0);
                Assert.True(goal.CurrentAmount > 0);
                Assert.True(goal.Contribution > 0);
                Assert.NotNull(goal.ContributionFrequencyName);
                Assert.NotNull(goal.ContributionFrequency);
                Assert.False(goal.IsDeleted);

                if (goal.PlannedDate != null)
                    Assert.NotEqual(new DateTime(), goal.PlannedDate);
            }
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var newAmount = 250000M;
            var goal = await _repo.UpdateAsync(
                id,
                new GoalDto
                {
                    Amount = newAmount
                });

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.NotNull(goal.TypeName);
            Assert.NotNull(goal.Type);
            Assert.True(goal.Amount > 0);
            Assert.Equal(newAmount, goal.Amount);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.Contribution > 0);
            Assert.NotNull(goal.ContributionFrequencyName);
            Assert.NotNull(goal.ContributionFrequency);
            Assert.False(goal.IsDeleted);

            if (goal.PlannedDate != null)
                Assert.NotEqual(new DateTime(), goal.PlannedDate);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            await _repo.DeleteAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }
    }
}