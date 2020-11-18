using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL.Types;
using GraphQL.Utilities;

namespace aiof.api.services
{
    public class UserSchema : Schema
    {
        public UserSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<UserQuery>();
        }
    }
}
