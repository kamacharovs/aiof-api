using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

using Microsoft.Extensions.Logging;

using GraphQL;
using GraphQL.Types;

using aiof.api.data;
using aiof.api.data.graphql;

namespace aiof.api.services
{
    public class GraphQLRepository : IGraphQLRepository
    {
        private readonly ILogger<GraphQLRepository> _logger;
        private readonly ITenant _tenant;
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;

        public GraphQLRepository(
            ILogger<GraphQLRepository> logger,
            ITenant tenant,
            ISchema schema,
            IDocumentExecuter executer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
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
                var errors = result.Errors.Select(x => x.Message);
                var message = string.Join(", ", errors);

                throw new AiofFriendlyException(
                    HttpStatusCode.BadRequest,
                    message);
            }

            _logger.LogInformation("{Tenant} | GraphQL command executed. Type={GraphQLType} | Request={GraphQLRequest}",
                _tenant.Log,
                result.Operation.OperationType,
                query.Query);

            return result.Data;
        }
    }
}
