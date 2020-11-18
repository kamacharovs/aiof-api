using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GraphQL;
using GraphQL.Types;

using aiof.api.data;

namespace aiof.api.services
{
    public class GraphQLRepository : IGraphQLRepository
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        public GraphQLRepository(
            ISchema schema,
            IDocumentExecuter executer)
        {
            _schema = schema ?? throw new ArgumentNullException(nameof(schema));
            _executer = executer ?? throw new ArgumentNullException(nameof(executer));
        }

        public async Task<object> ExecuteAsync(GraphQLQuery query)
        {
            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
            });

            if (result.Errors?.Count > 0)
            {
                throw new AiofFriendlyException();
            }

            return result.Data;
        }
    }
}
