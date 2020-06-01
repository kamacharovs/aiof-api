﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using aiof.api.data;

namespace aiof.api.services
{
    public class AiofRepository : IAiofRepository
    {
        private readonly AiofContext _context;
        private readonly ILogger<AiofRepository> _logger;

        public AiofRepository(AiofContext context, ILogger<AiofRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<IAsset>> GetAssets()
        {
            return await _context.Assets
                .ToListAsync();
        }

        private IQueryable<Asset> GetAssetsQuery()
        {
            return _context.Assets
                .Include(x => x.Type)
                .AsNoTracking()
                .AsQueryable();
        }

        private IQueryable<Finance> GetFinancesQuery()
        {
            return _context.Finances
                .Include(x => x.User)
                .Include(x => x.Assets)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Liabilities)
                    .ThenInclude(x => x.Type)
                .Include(x => x.Goals)
                    .ThenInclude(x => x.Type)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<IAsset> GetAssetAsync(int id)
        {
            return await GetAssetsQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IFinance> GetFinanceAsync(int id)
        {
            return await GetFinancesQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }        
    }
}
