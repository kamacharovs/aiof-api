using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using aiof.api.services;
using aiof.api.data;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("throw/500")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public void Throw500()
        {
            throw new AiofFriendlyException("test 500");
        }

        [HttpGet]
        [Route("throw/400")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void Throw400()
        {
            throw new AiofFriendlyException(HttpStatusCode.BadRequest, "test 400");
        }
    }
}