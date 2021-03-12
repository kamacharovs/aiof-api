using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IGoalRepository
    {
        Task<IGoal> GetAsync(
            int id,
            bool asNoTracking = true);
        Task<IGoal> GetAsync(
            GoalDto dto,
            bool asNoTracking = true);
        Task<IEnumerable<IGoal>> GetAsync(
            GoalType type,
            bool asNoTracking = true);
        Task<IEnumerable<IGoal>> GetAllAsync(bool asNoTracking = true);
        Task<IEnumerable<object>> GetAllAsObjectsAsync(bool asNoTracking = true);
        Task<IGoal> AddAsync(string dtoStr);
        Task<IGoal> AddAsync(GoalDto dto);
        Task<IGoal> AddAsync(GoalTripDto dto);
        Task<IGoal> AddAsync(GoalHomeDto dto);
        Task<IGoal> UpdateAsync(
            int id, 
            GoalDto dto);
        Task DeleteAsync(int id);
        DateTime? CalculateProjectedDate(
            decimal? amount,
            decimal? currentAmount,
            decimal? monthlyContribution);
    }
}