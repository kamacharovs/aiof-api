using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.FeatureManagement.Mvc;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [FeatureGate(FeatureFlags.Goal)]
    [Authorize]
    [ApiController]
    [Route("goal")]
    [Produces(Constants.ApplicationJson)]
    [Consumes(Constants.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status401Unauthorized)]
    public class GoalController : ControllerBase
    {
        public readonly IGoalRepository _repo;

        public GoalController(IGoalRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>
        /// Get Goal by id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalAsync([FromRoute, Required] int id)
        {
            return Ok(await _repo.GetAsync(id));
        }

        /// <summary>
        /// Get Goals
        /// </summary>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<IGoal>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _repo.GetAllAsObjectsAsync());
        }

        /// <summary>
        /// Add Goal
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddGoalAsync()
        {
            return Created(nameof(Goal), await _repo.AddAsync(await Request.GetRawBodyStringAsync()));
        }

        /// <summary>
        /// Update Goal by id
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateGoalAsync(
            [FromRoute, Required] int id, 
            [FromBody, Required] GoalDto dto)
        {
            return Ok(await _repo.UpdateAsync(id, dto));
        }

        /// <summary>
        /// Delete Goal by id
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute, Required] int id)
        {
            await _repo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get Goal types
        /// </summary>
        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public IActionResult GetGoalTypes()
        {
            return Ok(Constants.GoalTypes);
        }

        /// <summary>
        /// Get Goal trip types
        /// </summary>
        [HttpGet]
        [Route("trip/types")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public IActionResult GetGoalTripTypes()
        {
            return Ok(Constants.GoalTripTypes);
        }
    }
}