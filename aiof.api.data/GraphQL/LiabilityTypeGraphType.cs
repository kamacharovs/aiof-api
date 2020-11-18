using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class LiabilityTypeGraphType : ObjectGraphType<LiabilityType>, IGraphType
    {
        public LiabilityTypeGraphType()
        {
            Field(x => x.Name);
            Field(x => x.PublicKey);
        }
    }
}
