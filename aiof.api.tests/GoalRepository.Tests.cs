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
    }
}