using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using Newtonsoft.Json;

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
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task GetAsync_ById_NotFound(int id, int userId)
        {
            var repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => repo.GetAsync(id * 100));
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsType), MemberType = typeof(Helper))]
        public async Task GetAsync_ByType_IsSuccessful(GoalType type)
        {
            var _repo = new ServiceHelper().GetRequiredService<IGoalRepository>();
            var goals = await _repo.GetAsync(type);
            var goal = goals.FirstOrDefault();

            Assert.NotEmpty(goals);
            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.True(goal.UserId > 0);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
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
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task GetAllAsync_IsEmpty(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId * 100 }.GetRequiredService<IGoalRepository>();
            var goals = await _repo.GetAllAsync();

            Assert.Empty(goals);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Generic_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = JsonConvert.SerializeObject(Helper.RandomGoalDto());
            var goal = await _repo.AddAsync(dto);

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
        }
        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_GoalDto_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = Helper.RandomGoalDto();
            var goal = await _repo.AddAsync(dto);

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Trip_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = JsonConvert.SerializeObject(Helper.RandomGoalTripDto());
            var goal = (await _repo.AddAsync(dto)) as GoalTrip;

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Equal(GoalType.Trip, goal.Type);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
            Assert.NotNull(goal.Destination);
            Assert.Contains(goal.TripType, Constants.GoalTripTypes);
            Assert.True(goal.Duration >= 0);
            Assert.True(goal.Travelers >= 0);
            Assert.True(goal.Flight >= 0);
            Assert.True(goal.Hotel >= 0);
            Assert.True(goal.Car >= 0);
            Assert.True(goal.Food >= 0);
            Assert.True(goal.Activities >= 0);
            Assert.True(goal.Other >= 0);
        }
        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_GoalTripDto_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = Helper.RandomGoalTripDto();
            var goal = (await _repo.AddAsync(dto)) as GoalTrip;

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Equal(GoalType.Trip, goal.Type);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
            Assert.NotNull(goal.Destination);
            Assert.Contains(goal.TripType, Constants.GoalTripTypes);
            Assert.True(goal.Duration >= 0);
            Assert.True(goal.Travelers >= 0);
            Assert.True(goal.Flight >= 0);
            Assert.True(goal.Hotel >= 0);
            Assert.True(goal.Car >= 0);
            Assert.True(goal.Food >= 0);
            Assert.True(goal.Activities >= 0);
            Assert.True(goal.Other >= 0);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_Home_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = JsonConvert.SerializeObject(Helper.RandomGoalHomeDto());
            var goal = (await _repo.AddAsync(dto)) as GoalHome;

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Equal(GoalType.BuyAHome, goal.Type);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
            Assert.True(goal.HomeValue >= 0);
            Assert.True(goal.MortgageRate >= 0);
            Assert.True(goal.PercentDownPayment >= 0);
            Assert.True(goal.AnnualInsurance >= 0);
            Assert.True(goal.AnnualPropertyTax >= 0);
            Assert.True(goal.RecommendedAmount >= 0);
        }
        [Theory]
        [MemberData(nameof(Helper.GoalsUserId), MemberType = typeof(Helper))]
        public async Task AddAsync_GoalHomeDto_IsSuccessful(int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var dto = Helper.RandomGoalHomeDto();
            var goal = (await _repo.AddAsync(dto)) as GoalHome;

            Assert.NotNull(goal);
            Assert.NotNull(goal.Name);
            Assert.Equal(GoalType.BuyAHome, goal.Type);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
            Assert.True(goal.HomeValue >= 0);
            Assert.True(goal.MortgageRate >= 0);
            Assert.True(goal.PercentDownPayment >= 0);
            Assert.True(goal.AnnualInsurance >= 0);
            Assert.True(goal.AnnualPropertyTax >= 0);
            Assert.True(goal.RecommendedAmount >= 0);
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
            Assert.Contains(goal.Type, Constants.GoalTypes);
            Assert.Equal(goal.UserId, userId);
            Assert.True(goal.Amount > 0);
            Assert.True(goal.CurrentAmount > 0);
            Assert.True(goal.MonthlyContribution > 0);
            Assert.NotEqual(DateTime.UtcNow, goal.PlannedDate);
            Assert.False(goal.IsDeleted);
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_NotFound(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            var newAmount = 250000M;
            
            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.UpdateAsync(
                id * 100,
                new GoalDto
                {
                    Amount = newAmount
                }));
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task UpdateAsync_ById_DtoIsNull_Throws_BadRequest(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            
            await Assert.ThrowsAsync<AiofFriendlyException>(() => _repo.UpdateAsync(id, null));
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_IsSuccessful(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();
            await _repo.DeleteAsync(id);

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.GetAsync(id));
        }

        [Theory]
        [MemberData(nameof(Helper.GoalsIdUserId), MemberType = typeof(Helper))]
        public async Task DeleteAsync_ById_NotFound(int id, int userId)
        {
            var _repo = new ServiceHelper() { UserId = userId }.GetRequiredService<IGoalRepository>();

            await Assert.ThrowsAsync<AiofNotFoundException>(() => _repo.DeleteAsync(id * 100));
        }
    }
}