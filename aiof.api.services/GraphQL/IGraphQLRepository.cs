using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IGraphQLRepository
    {
        Task<object> ExecuteAsync(GraphQLQuery query);
    }
}
