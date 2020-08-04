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
        [Route("user/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUserAsync([FromRoute]int id, [FromBody]UserDto userDto)
        {
            return Created(nameof(User), await _repo.UpsertFinanceAsync(id, userDto));
        }
    }
}
