using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.FeatureManagement.Mvc;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [FeatureGate(FeatureFlags.GraphQL)]
    [Authorize]
    [Route("graphql")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly IGraphQLRepository _repo;

        public GraphQLController(IGraphQLRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            return Ok(await _repo.ExecuteAsync(query));
        }
    }
}
