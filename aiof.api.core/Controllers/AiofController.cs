using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AiofController : ControllerBase
    {
        public AiofController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("all in one finance");
        }
    }
}
