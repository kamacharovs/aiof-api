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
    /// Everything aiof user
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("user")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(IAiofProblemDetailBase), StatusCodes.Status401Unauthorized)]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        /// <summary>
        /// Get User
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _repo.GetAsync());
        }
        
        /// <summary>
        /// Get User by username
        /// </summary>
        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsernameAsync([FromRoute] string username)
        {
            return Ok(await _repo.GetAsync(username));
        }

        /// <summary>
        /// Upsert User
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertAsync([FromBody, Required] UserDto userDto)
        {
            return Ok(await _repo.UpsertAsync(userDto));
        }

        /// <summary>
        /// Get User profile
        /// </summary>
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUserProfile), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfileAsync()
        {
            return Ok(await _repo.GetProfileAsync());
        }

        /// <summary>
        /// Upsert User profile
        /// </summary>
        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IUserProfile), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertUserProfileAsync([FromBody, Required] UserProfileDto userProfileDto)
        {
            return Ok(await _repo.UpsertProfileAsync(userProfileDto));
        }

        /// <summary>
        /// Get Subscription by id
        /// </summary>
        [HttpGet]
        [Route("subscription/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ISubscription), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubscriptionAsync([FromRoute, Required] int id)
        {
            return Ok(await _repo.GetSubscriptionAsync(id));
        }

        /// <summary>
        /// Add Subscription
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
        /// Upsert Subscription
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

        /// <summary>
        /// Delete Subscription
        /// </summary>
        [HttpDelete]
        [Route("subscription/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ISubscription), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSubscriptionAsync(
            [FromRoute, Required] int id)
        {
            await _repo.DeleteSubscriptionAsync(id);

            return Ok();
        }

        /// <summary>
        /// Get Account by id
        /// </summary>
        [FeatureGate(FeatureFlags.Account)]
        [HttpGet]
        [Route("account/{id}")]
        [ProducesResponseType(typeof(IAccount), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountAsync([FromRoute, Required] int id)
        {
            return Ok(await _repo.GetAccountAsync(id));
        }

        /// <summary>
        /// Add Account
        /// </summary>
        [FeatureGate(FeatureFlags.Account)]
        [HttpPost]
        [Route("account")]
        [ProducesResponseType(typeof(IAccount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAccountAsync([FromBody, Required] AccountDto accountDto)
        {
            return Ok(await _repo.AddAccountAsync(accountDto));
        }

        /// <summary>
        /// Update Account
        /// </summary>
        [FeatureGate(FeatureFlags.Account)]
        [HttpPut]
        [Route("account/{id}")]
        [ProducesResponseType(typeof(IAccount), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAccountAsync(
            [FromRoute, Required] int id,
            [FromBody, Required] AccountDto accountDto)
        {
            return Ok(await _repo.UpdateAccountAsync(id, accountDto));
        }

        /// <summary>
        /// Get Account types
        /// </summary>
        [FeatureGate(FeatureFlags.Account)]
        [HttpGet]
        [Route("account/types")]
        [ProducesResponseType(typeof(IEnumerable<IAccountType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountTypesAsync()
        {
            return Ok(await _repo.GetAccountTypesAsync());
        }

        /// <summary>
        /// Get Account types mapping
        /// </summary>
        [FeatureGate(FeatureFlags.Account)]
        [HttpGet]
        [Route("account/types/map")]
        [ProducesResponseType(typeof(IEnumerable<IAccountTypeMap>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountTypeMapsAsync()
        {
            return Ok(await _repo.GetAccountTypeMapsAsync());
        }

        /// <summary>
        /// Get Education levels
        /// </summary>
        [HttpGet]
        [Route("education/levels")]
        [ProducesResponseType(typeof(IEnumerable<IEducationLevel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEducationLevelsAsync()
        {
            return Ok(await _repo.GetEducationLevelsAsync());
        }

        /// <summary>
        /// Get Marital statuses
        /// </summary>
        [HttpGet]
        [Route("marital/statuses")]
        [ProducesResponseType(typeof(IEnumerable<IMaritalStatus>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMaritalStatusesAsync()
        {
            return Ok(await _repo.GetMaritalStatusesAsync());
        }

        /// <summary>
        /// Get Residential statuses
        /// </summary>
        [HttpGet]
        [Route("residential/statuses")]
        [ProducesResponseType(typeof(IEnumerable<IResidentialStatus>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResidentialStatusesAsync()
        {
            return Ok(await _repo.GetResidentialStatusesAsync());
        }

        /// <summary>
        /// Get Household adults
        /// </summary>
        [HttpGet]
        [Route("household/adults")]
        [ProducesResponseType(typeof(IEnumerable<IHouseholdAdult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHouseholdAdultsAsync()
        {
            return Ok(await _repo.GetHouseholdAdultsAsync());
        }

        /// <summary>
        /// Get Household children
        /// </summary>
        [HttpGet]
        [Route("household/children")]
        [ProducesResponseType(typeof(IEnumerable<IHouseholdChild>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHouseholdChildrenAsync()
        {
            return Ok(await _repo.GetHouseholdChildrenAsync());
        }
    }
}
