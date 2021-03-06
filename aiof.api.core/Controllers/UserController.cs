﻿using System;
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
    [Produces(Constants.ApplicationJson)]
    [Consumes(Constants.ApplicationJson)]
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
        /// Get User by email
        /// </summary>
        [HttpGet]
        [Route("{email}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByEmailAsync([FromRoute, Required] string email)
        {
            return Ok(await _repo.GetAsync(email));
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
        /// Get User dependents
        /// </summary>
        [FeatureGate(FeatureFlags.UserDependent)]
        [HttpGet]
        [Route("dependents")]
        [ProducesResponseType(typeof(IEnumerable<IUserDependent>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDependentsAsync()
        {
            return Ok(await _repo.GetDependentsAsync());
        }

        /// <summary>
        /// Get User dependent by id
        /// </summary>
        [FeatureGate(FeatureFlags.UserDependent)]
        [HttpGet]
        [Route("dependent/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUserDependent), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDependentAsync([FromRoute, Required] int id)
        {
            return Ok(await _repo.GetDependentAsync(id));
        }

        /// <summary>
        /// Add User dependent
        /// </summary>
        [FeatureGate(FeatureFlags.UserDependent)]
        [HttpPost]
        [Route("dependent")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IUserDependent), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDependentAsync([FromBody, Required] UserDependentDto userDependentDto)
        {
            return Ok(await _repo.AddDependentAsync(userDependentDto));
        }

        /// <summary>
        /// Delete User dependent by id
        /// </summary>
        [FeatureGate(FeatureFlags.UserDependent)]
        [HttpDelete]
        [Route("dependent/{id}")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IUserDependent), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDependentAsync([FromRoute, Required] int id)
        {
            await _repo.DeleteDependentAsync(id);
            return Ok();
        }

        /// <summary>
        /// Get User dependent relationships
        /// </summary>
        /// <returns></returns>
        [FeatureGate(FeatureFlags.UserDependent)]
        [HttpGet]
        [Route("dependent/relationships")]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public IActionResult GetUserRelationships()
        {
            return Ok(Constants.UserRelationships);
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
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IUserProfile), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertUserProfileAsync([FromBody, Required] UserProfileDto userProfileDto)
        {
            return Ok(await _repo.UpsertProfileAsync(userProfileDto));
        }

        /// <summary>
        /// Upsert User profile physical address
        /// </summary>
        [HttpPut]
        [Route("profile/physical/address")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IAddress), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertProfilePhysicalAddressAsync([FromBody] AddressDto addressDto)
        {
            return Ok(await _repo.UpsertProfilePhysicalAddressAsync(addressDto));
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

        /// <summary>
        /// Get User profile options
        /// </summary>
        [HttpGet]
        [Route("profile/options")]
        [ProducesResponseType(typeof(IUserProfileOptions), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProfileOptionsAsync()
        {
            return Ok(await _repo.GetUserProfileOptionsAsync());
        }
    }
}
