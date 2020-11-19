using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;

namespace aiof.api.data.graphql
{
    public class AccountGraphType : ObjectGraphType<Account>, IGraphType
    {
        public AccountGraphType()
        {
            Field(x => x.Id);
            Field(x => x.PublicKey);
            Field(x => x.Name);
            Field(x => x.Description);
            Field(x => x.TypeName);
            Field(x => x.UserId);
            Field(x => x.IsDeleted);
        }
    }
}
