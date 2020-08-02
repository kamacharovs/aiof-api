using System;
using System.Threading.Tasks;

namespace aiof.api.data
{
    public static class EfExtensionMethods
    {
        public static async Task<Goal> UpdateGoalAsync(this Goal goal, 
            AiofContext context, 
            string name)
        {
            goal.Name = name;

            context.Goals
                .Update(goal);

            await context.SaveChangesAsync();

            return goal;
        }
    }
}