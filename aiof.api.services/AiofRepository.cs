using System;
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

        private IQueryable<Liability> GetLiabilitiesQuery()
        {
            return _context.Liabilities
                .Include(x => x.Type)
                .AsNoTracking()
                .AsQueryable();
        }

        private IQueryable<Goal> GetGoalsQuery()
        {
            return _context.Goals
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

        public async Task<IEnumerable<IAsset>> GetAssetsAsync(string typeName)
        {
            return await GetAssetsQuery()
                .Where(x => x.TypeName == typeName)
                .OrderBy(x => x.TypeName)
                .ToListAsync();
        }

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            await _context.Assets
                .AddAsync(asset);

            await _context.SaveChangesAsync();

            return asset;
        }

        public async IAsyncEnumerable<Asset> AddAssetsAsync(IEnumerable<Asset> assets)
        {
            foreach (var asset in assets)
                yield return await AddAssetAsync(asset);
        }

        public async Task<ILiability> GetLiabilityAsync(int id)
        {
            return await GetLiabilitiesQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Liability> AddLiabilityAsync(Liability liability)
        {
            await _context.Liabilities
                .AddAsync(liability);

            await _context.SaveChangesAsync();

            return liability;
        }

        public async IAsyncEnumerable<Liability> AddLiabilitiesAsync(IEnumerable<Liability> liabilities)
        {
            foreach (var liability in liabilities)
                yield return await AddLiabilityAsync(liability);
        }

        public async Task<IGoal> GetGoalAsync(int id)
        {
            return await GetGoalsQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Goal> AddGoalAsync(Goal goal)
        {
            await _context.Goals
                .AddAsync(goal);

            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<IFinance> GetFinanceAsync(int id)
        {
            return await GetFinancesQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IFinance> AddFinanceAsync(int userId,
            IEnumerable<Asset> assets,
            IEnumerable<Liability> liabilities,
            IEnumerable<Goal> goals)
        {
            var finance = await _context.Finances
                .AddAsync(new Finance()
                {
                    UserId = userId
                });

            await _context.SaveChangesAsync();

            return finance.Entity;
        }
    }
}
