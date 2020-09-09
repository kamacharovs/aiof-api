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
        
        /// <summary>
        /// Get User by username
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromQuery] string username)
        {
            return Ok(await _repo.GetUserAsync(username));
        }

        /// <summary>
        /// Upsert a User profile
        /// </summary>
        [HttpPut]
        [Route("profile")]
        public async Task<IActionResult> AddUserProfileAsync(
            [FromQuery, Required] string username,
            [FromBody, Required] UserProfileDto userProfileDto)
        {
            return Ok(await _repo.UpsertUserProfileAsync(username, userProfileDto));
        }

        /// <summary>
        /// Get Subscriptions by public key
        /// </summary>
        [HttpGet]
        [Route("subscriptions/{publicKey}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ISubscription), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubscriptionAsync([FromRoute, Required] Guid publicKey)
        {
            return Ok(await _repo.GetSubscriptionAsync(publicKey));
        }

        /// <summary>
        /// Add a Subscription
        /// </summary>
        [HttpPost]
        [Route("subscription")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ISubscription), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSubscriptionAsync([FromBody, Required] SubscriptionDto subscriptionDto)
        {
            return Ok(await _repo.AddSubscriptionAsync(subscriptionDto));
        }

        /// <summary>
        /// Upsert a Subscription
        /// </summary>
        [HttpPut]
        [Route("subscription/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ISubscription), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSubscriptionAsync(
            [FromRoute, Required] int id,
            [FromBody, Required] SubscriptionDto subscriptionDto)
        {
            return Ok(await _repo.UpdateSubscriptionAsync(id, subscriptionDto));
        }
    }
}
