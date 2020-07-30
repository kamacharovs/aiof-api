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
    [FeatureGate(FeatureFlags.Asset)]
    [ApiController]
    [Route("asset")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AssetController : ControllerBase
    {
        public readonly IAssetRepository _repo;

        public AssetController(IAssetRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetAssetAsync(id));
        }

        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(IEnumerable<IAssetType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetTypesAsync()
        {
            return Ok(await _repo.GetAssetTypesAsync());
        }

        [HttpPost]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAssetAsync([FromBody]AssetDto assetDto)
        {
            return Created(nameof(Asset), await _repo.AddAssetAsync(assetDto));
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAssetAsync([FromRoute]int id, [FromBody]AssetDto assetDto)
        {
            return Ok(await _repo.UpdateAssetAsync(id, assetDto));
        }
    }
}