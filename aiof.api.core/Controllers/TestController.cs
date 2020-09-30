using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using FluentValidation;
using FluentValidation.Results;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("test")]
    [Produces(Keys.ApplicationJson)]
    [Consumes(Keys.ApplicationJson)]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("throw/500")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status500InternalServerError)]
        public void Throw500()
        {
            throw new AiofFriendlyException("InternalServerError");
        }

        [HttpGet]
        [Route("throw/400")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        public void Throw400()
        {
            throw new AiofFriendlyException(HttpStatusCode.BadRequest, "BadRequest");
        }

        [HttpGet]
        [Route("throw/401")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status401Unauthorized)]
        public void Throw401()
        {
            throw new AiofFriendlyException(HttpStatusCode.Unauthorized, "Unauthorized");
        }

        [HttpGet]
        [Route("throw/403")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status403Forbidden)]
        public void Throw403()
        {
            throw new AiofFriendlyException(HttpStatusCode.Forbidden, "Forbidden");
        }

        [HttpGet]
        [Route("throw/validation")]
        [ProducesResponseType(typeof(IAiofProblemDetail), StatusCodes.Status400BadRequest)]
        public void ThrowValidation()
        {
            throw new ValidationException($"Validation error", 
            new List<ValidationFailure>
            {
                new ValidationFailure("Username", "Username must be present"),
                new ValidationFailure("Password", "Password must contain an upper case letter")
            });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        [Route("admin/authorization")]
        public IActionResult AdminAuthorization()
        {
            return Ok("Admin is Authorized");
        }
        [Authorize(Roles = Roles.User)]
        [HttpGet]
        [Route("user/authorization")]
        public IActionResult UserAuthorization()
        {
            return Ok("User is Authorized");
        }
        [Authorize(Roles = Roles.AdminOrUser)]
        [HttpGet]
        [Route("admin/user/authorization")]
        public IActionResult AdminOrUserAuthorization()
        {
            return Ok("AdminOrUser is Authorized");
        }
    }
}