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
    [FeatureGate(FeatureFlags.Asset)]
    [Authorize]
    [ApiController]
    [Route("asset")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    public class AssetController : ControllerBase
    {
        public readonly IAssetRepository _repo;

        public AssetController(IAssetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("{publicKey}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetAsync([FromRoute] Guid publicKey)
        {
            return Ok(await _repo.GetAsync(publicKey));
        }

        [HttpPut]
        [Route("{publicKey}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAssetAsync([FromRoute] Guid publicKey, [FromBody] AssetDto assetDto)
        {
            return Ok(await _repo.UpdateAssetAsync(publicKey, assetDto));
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<IAssetType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetTypesAsync()
        {
            return Ok(await _repo.GetAssetTypesAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAssetAsync([FromBody] AssetDto assetDto)
        {
            return Created(nameof(Asset), await _repo.AddAssetAsync(assetDto));
        }

        [HttpDelete]
        [Route("{publicKey}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IEnumerable<IAssetType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAsync([FromRoute, Required] Guid publicKey)
        {
            await _repo.DeleteAsync(publicKey);

            return Ok();
        }
    }
}