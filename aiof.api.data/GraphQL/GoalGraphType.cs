using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class GoalGraphType : ObjectGraphType<Goal>, IGraphType
    {
        public GoalGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.Name);
            Field(x => x.TypeName);
            Field(x => x.Amount);
            Field(x => x.CurrentAmount);
            Field(x => x.Contribution);
            Field(x => x.ContributionFrequencyName);
            Field(x => x.PlannedDate, nullable: true);
            Field(x => x.UserId, nullable: true);
            Field(x => x.IsDeleted);

            Field<GoalTypeGraphType>("type", resolve: context => context.Source.Type);
            Field<FrequencyGraphType>("frequency", resolve: context => context.Source.ContributionFrequency);
        }
    }
}
