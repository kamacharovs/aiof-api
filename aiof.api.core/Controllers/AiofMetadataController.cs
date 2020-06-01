using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using aiof.api.services;

namespace aiof.api.core.Controllers
{
    [ApiController]
    [Route("aiof/metadata")]
    public class AiofMetadataController : ControllerBase
    {
        public readonly IAiofMetadataRepository _repo;

        public AiofMetadataController(IAiofMetadataRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("frequencies")]
        public async Task<IActionResult> GetFrequenciesAsync()
        {
            return Ok(await _repo.GetFrequenciesAsync());
        }
    }
}