using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class UserGraphType : ObjectGraphType<User>, IGraphType
    {
        public UserGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email);
            Field(x => x.Username);
            Field(x => x.Created);

            Field<UserProfileGraphType>("profile", resolve: context => context.Source.Profile);

            Field<ListGraphType<AssetGraphType>>("assets", resolve: context => context.Source.Assets);
            Field<ListGraphType<LiabilityGraphType>>("liabilities", resolve: context => context.Source.Liabilities);
            Field<ListGraphType<GoalGraphType>>("goals", resolve: context => context.Source.Goals);
            Field<ListGraphType<SubscriptionGraphType>>("subscriptions", resolve: context => context.Source.Subscriptions);
            Field<ListGraphType<AccountGraphType>>("accounts", resolve: context => context.Source.Accounts);
        }
    }
}
