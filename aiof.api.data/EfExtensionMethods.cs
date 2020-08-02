using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public static class EfExtensionMethods
    {
        public static async Task<Goal> UpdateGoalAsync(this AiofContext context, 
            Goal goal, 
            string name)
        {
            goal.Name = name;

            context.Goals
                .Update(goal);

            await context.SaveChangesAsync();

            return goal;
        }

        public static async Task<Goal> UpdateGoalAsync(this AiofContext context, 
            int id,
            string name)
        {
            var goal = await context.Goals
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException();

            goal.Name = name;

            context.Goals
                .Update(goal);

            await context.SaveChangesAsync();

            return goal;
        }
    }
}