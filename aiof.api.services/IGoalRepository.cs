using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IGoalRepository
    {
        Task<IGoal> GetGoalAsync(int id);
        Task<IEnumerable<IGoalType>> GetGoalTypesAsync();
        Task<IGoal> AddGoalAsync(GoalDto goalDto);
        IAsyncEnumerable<IGoal> AddGoalsAsync(IEnumerable<GoalDto> goalDtos);
        Task<IGoal> UpdateGoalAsync(
            int id, 
            GoalDto goalDto);
    }
}