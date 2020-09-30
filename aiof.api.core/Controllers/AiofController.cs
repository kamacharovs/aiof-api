using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [Authorize]
    [ApiController]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    public class AiofController : ControllerBase
    {
        public readonly IAiofRepository _repo;

        public AiofController(IAiofRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpPost]
        [Route("user/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertFinanceAsync([FromRoute]int id, [FromBody]UserDto userDto)
        {
            return Ok(await _repo.UpsertFinanceAsync(id, userDto));
        }

        [HttpGet]
        [Route("frequencies")]
        [ProducesResponseType(typeof(IEnumerable<IFrequency>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFrequenciesAsync()
        {
            return Ok(await _repo.GetFrequenciesAsync());
        }
    }
}
