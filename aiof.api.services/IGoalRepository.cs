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
            GoalDto goalDto,
            bool asNoTracking = true);
        Task<IEnumerable<IGoal>> GetAsync(
            string typeName,
            bool asNoTracking = true);
        Task<IEnumerable<IGoal>> GetAllAsync(bool asNoTracking = true);
        Task<IGoalType> GetTypeAsync(
            string typeName,
            bool asNoTracking = true);
        Task<IEnumerable<IGoalType>> GetTypesAsync();
        Task<IGoal> AddAsync(GoalDto goalDto);
        IAsyncEnumerable<IGoal> AddAsync(IEnumerable<GoalDto> goalDtos);
        Task<IGoal> UpdateAsync(
            int id, 
            GoalDto goalDto);
        Task DeleteAsync(int id);
    }
}