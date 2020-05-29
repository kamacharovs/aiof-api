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
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetAssets());
        }

        [HttpGet]
        [Route("finance")]
        public async Task<IActionResult> GetFinanceAsync()
        {
            return Ok(await _repo.GetFinancesAsync());
        }
    }
}
