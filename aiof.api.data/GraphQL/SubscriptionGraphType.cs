using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class SubscriptionGraphType : ObjectGraphType<Subscription>, IGraphType
    {
        public SubscriptionGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.Amount);
            Field(x => x.PaymentFrequencyName);
            Field(x => x.PaymentLength);
            Field(x => x.From);
            Field(x => x.Url);
            Field(x => x.UserId);
            Field(x => x.IsDeleted);

            Field<FrequencyGraphType>("frequency", resolve: context => context.Source.PaymentFrequency);
        }
    }
}
