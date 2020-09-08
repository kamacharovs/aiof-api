using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement.Mvc;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [FeatureGate(FeatureFlags.Goal)]
    [ApiController]
    [Route("goal")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    public class GoalController : ControllerBase
    {
        public readonly IGoalRepository _repo;

        public GoalController(IGoalRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetGoalAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGoalAsync([FromRoute]int id, [FromBody]GoalDto goalDto)
        {
            return Ok(await _repo.UpdateGoalAsync(id, goalDto));
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<IGoalType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalTypesAsync()
        {
            return Ok(await _repo.GetGoalTypesAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddGoalAsync([FromBody]GoalDto goalDto)
        {
            return Created(nameof(Goal), await _repo.AddGoalAsync(goalDto));
        }

        [HttpDelete]
        [Route("{publicKey}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid publicKey)
        {
            await _repo.DeleteAsync(publicKey);
            return Ok();
        }
    }
}