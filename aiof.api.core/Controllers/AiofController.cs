using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using aiof.api.services;
using aiof.api.data;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("aiof")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AiofController : ControllerBase
    {
        public readonly IAiofRepository _repo;

        public AiofController(IAiofRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("user/{id}")]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetUserAsync(id));
        }

        [HttpGet]
        [Route("user/username/{username}")]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute]string username)
        {
            return Ok(await _repo.GetUserAsync(username));
        }

        [HttpPost]
        [Route("user/add")]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserAsync([FromBody]UserDto user)
        {
            return Created("user", await _repo.AddUserAsync(user));
        }


        [HttpGet]
        [Route("finance/{id}")]
        [ProducesResponseType(typeof(IFinance), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFinanceAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetFinanceAsync(id));
        }


        /// <summary>
        /// Adds a specific set of assets, liabilities and golas as a Finance object
        /// </summary>
        [HttpPost]
        [Route("finance/add")]
        [ProducesResponseType(typeof(IFinance), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddFinanceAsync([FromBody]FinanceDto finance)
        {
            return Created("finance", await _repo.AddFinanceAsync(finance));
        }


        [HttpGet]
        [Route("asset/{id}")]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetAssetAsync(id));
        }

        [HttpGet]
        [Route("asset/types")]
        [ProducesResponseType(typeof(IEnumerable<IAssetType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssetTypesAsync()
        {
            return Ok(await _repo.GetAssetTypesAsync());
        }

        [HttpPost]
        [Route("asset/add")]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddAssetAsync([FromBody]AssetDto assetDto)
        {
            return Created("asset", await _repo.AddAssetAsync(assetDto));
        }

        [HttpPut]
        [Route("asset/{id}/update")]
        [ProducesResponseType(typeof(IAsset), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAssetAsync([FromRoute]int id, [FromBody]AssetDto assetDto)
        {
            return Ok(await _repo.UpdateAssetAsync(id, assetDto));
        }


        [HttpGet]
        [Route("liability/{id}")]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetLiabilityAsync(id));
        }

        [HttpGet]
        [Route("liability/types")]
        [ProducesResponseType(typeof(IEnumerable<ILiabilityType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLiabilityTypesAsync()
        {
            return Ok(await _repo.GetLiabilityTypesAsync());
        }

        [HttpPost]
        [Route("liability/add")]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddLiabilityAsync([FromBody]LiabilityDto liabilityDto)
        {
            return Created("liability", await _repo.AddLiabilityAsync(liabilityDto));
        }

        [HttpPut]
        [Route("liability/{id}/update")]
        [ProducesResponseType(typeof(ILiability), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLiabilityAsync([FromRoute]int id, [FromBody]LiabilityDto liabilityDto)
        {
            return Ok(await _repo.UpdateLiabilityAsync(id, liabilityDto));
        }


        [HttpGet]
        [Route("goal/{id}")]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetGoalAsync(id));
        }

        [HttpGet]
        [Route("goal/types")]
        [ProducesResponseType(typeof(IEnumerable<IGoalType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGoalTypesAsync()
        {
            return Ok(await _repo.GetGoalTypesAsync());
        }

        [HttpPost]
        [Route("goal/add")]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddGoalAsync([FromBody]GoalDto goalDto)
        {
            return Created("goal", await _repo.AddGoalAsync(goalDto));
        }

        [HttpPut]
        [Route("goal/{id}/update")]
        [ProducesResponseType(typeof(IGoal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGoalAsync([FromRoute]int id, [FromBody]GoalDto goalDto)
        {
            return Ok(await _repo.UpdateGoalAsync(id, goalDto));
        }
    }
}
