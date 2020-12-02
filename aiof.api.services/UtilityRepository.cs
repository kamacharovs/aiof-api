using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using aiof.api.data;

namespace aiof.api.services
{
    public class UtilityRepository : IUtilityRepository
    {
        private readonly ILogger<UtilityRepository> _logger;
        private readonly AiofContext _context;

        public UtilityRepository(
            ILogger<UtilityRepository> logger,
            AiofContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IQueryable<UsefulDocumentation> GetUsefulDocumentationQuery(bool asNoTracking = true)
        {
            var query = _context.UsefulDocumentations
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }

        public async Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsByPageAsync(
            string page,
            bool asNoTracking = true)
        {
            return await GetUsefulDocumentationQuery(asNoTracking)
                .Where(x => x.Page == page)
                .ToListAsync();
        }
        public async Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsByCategoryAsync(
            string category,
            bool asNoTracking = true)
        {
            return await GetUsefulDocumentationQuery(asNoTracking)
                .Where(x => x.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<IUsefulDocumentation>> GetUsefulDocumentationsAsync(
            string page = null,
            string category = null,
            bool asNoTracking = true)
        {
            if (!string.IsNullOrWhiteSpace(page))
                return await GetUsefulDocumentationsByPageAsync(page, asNoTracking);
            else if (!string.IsNullOrWhiteSpace(category))
                return await GetUsefulDocumentationsByCategoryAsync(category, asNoTracking);
            else
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Invalid request for GetUsefulDocumentationsAsync. Use page or category as parameters.");
        }
    }
}
