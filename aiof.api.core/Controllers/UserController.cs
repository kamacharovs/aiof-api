using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("user")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpPost]
        [Route("profile")]
        public async Task<IActionResult> UpdateUserProfileAsync(
            [FromBody, Required] UserProfileDto userProfileDto,
            [FromQuery, Required] string username)
        {
            return Ok(await _repo.UpdateUserProfileAsync(username, userProfileDto));
        }
    }
}
