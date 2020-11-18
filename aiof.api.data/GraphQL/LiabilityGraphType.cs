using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class LiabilityGraphType : ObjectGraphType<Liability>, IGraphType
    {
        public LiabilityGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.Name);
            Field(x => x.TypeName);
            Field(x => x.Value);
            Field(x => x.MonthlyPayment, nullable: true);
            Field(x => x.Years, nullable: true);
            Field(x => x.UserId);
            Field(x => x.IsDeleted);

            Field<LiabilityTypeGraphType>("type", resolve: context => context.Source.Type);
        }
    }
}
