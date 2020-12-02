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
    /// <summary>
    /// Everything utility
    /// </summary>
    [Authorize]
    [ApiController]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status401Unauthorized)]
    public class UtilityController : ControllerBase
    {
        public readonly IUtilityRepository _repo;

        public UtilityController(IUtilityRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        #region Useful documentation
        /// <summary>
        /// Get Useful documentation
        /// </summary>
        [HttpGet]
        [Route("useful/documentation")]
        [ProducesResponseType(typeof(IEnumerable<IUsefulDocumentation>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsefulDocumentationAsync(
            [FromQuery] string page, 
            [FromQuery] string category)
        {
            return Ok(await _repo.GetUsefulDocumentationsAsync(page, category));
        }
        #endregion
    }
}
