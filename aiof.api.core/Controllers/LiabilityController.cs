using System;
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
    [FeatureGate(FeatureFlags.Liability)]
    [Authorize]
    [ApiController]
    [Route("liability")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status401Unauthorized)]
    public class LiabilityController : ControllerBase
    {
        public readonly ILiabilityRepository _repo;

        public LiabilityController(ILiabilityRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>
        /// Get Liability by id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityAsync([FromRoute, Required] int id)
        {
            return Ok(await _repo.GetAsync(id));
        }

        /// <summary>
        /// Get Liabilities
        /// </summary>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(IEnumerable<ILiability>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _repo.GetAllAsync());
        }

        /// <summary>
        /// Update Liability by id
        /// </summary>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLiabilityAsync(
            [FromRoute, Required] int id, 
            [FromBody, Required] LiabilityDto liabilityDto)
        {
            return Ok(await _repo.UpdateAsync(id, liabilityDto));
        }

        /// <summary>
        /// Add Liability
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddLiabilityAsync([FromBody, Required] LiabilityDto liabilityDto)
        {
            return Created(nameof(Liability), await _repo.AddAsync(liabilityDto));
        }

        /// <summary>
        /// Delete Liability by id
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
        /// Add Liability types
        /// </summary>
        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<ILiabilityType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityTypesAsync()
        {
            return Ok(await _repo.GetTypesAsync());
        }
    }
}