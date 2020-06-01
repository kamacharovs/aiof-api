using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("aiof")]
    public class AiofController : ControllerBase
    {
        public readonly IAiofRepository _repo;

        public AiofController(IAiofRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("finance/{id}")]
        public async Task<IActionResult> GetFinanceAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetFinanceAsync(id));
        }

        [HttpGet]
        [Route("asset/{id}")]
        public async Task<IActionResult> GetAssetAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetAssetAsync(id));
        }
    }
}
