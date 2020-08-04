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
    }
}
