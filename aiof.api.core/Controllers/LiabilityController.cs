using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement.Mvc;

using aiof.api.services;
using aiof.api.data;

namespace aiof.api.core.Controllers
{
    [FeatureGate(FeatureFlags.Liability)]
    [ApiController]
    [Route("liability")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class LiabilityController : ControllerBase
    {
        public readonly ILiabilityRepository _repo;

        public LiabilityController(ILiabilityRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetLiabilityAsync(id));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateLiabilityAsync([FromRoute]int id, [FromBody]LiabilityDto liabilityDto)
        {
            return Ok(await _repo.UpdateLiabilityAsync(id, liabilityDto));
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<ILiabilityType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityTypesAsync()
        {
            return Ok(await _repo.GetLiabilityTypesAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddLiabilityAsync([FromBody]LiabilityDto liabilityDto)
        {
            return Created(nameof(Liability), await _repo.AddLiabilityAsync(liabilityDto));
        }
    }
}