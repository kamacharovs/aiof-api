using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private IQueryable<User> GetUsersQuery()
        {
            return _context.Users
                .AsNoTracking()
                .AsQueryable();
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

        public async Task<User> GetUserAsync(int id)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public async IAsyncEnumerable<Goal> AddGoalsAsync(IEnumerable<Goal> goals)
        {
            foreach (var goal in goals)
                yield return await AddGoalAsync(goal);
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
            //TODO check if all financeId's are the same
            var financeId = assets.FirstOrDefault()
                .FinanceId;

            if (!assets.All(x => x.FinanceId == financeId)
                || !liabilities.All(x => x.FinanceId == financeId)
                || !goals.All(x => x.FinanceId == financeId))
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"all assets, liabilities and goals must have the same 'FinanceId'");
            }

            var finance = await _context.Finances
                .AddAsync(new Finance()
                {
                    UserId = userId
                });

            await _context.SaveChangesAsync();

            await foreach (var asset in AddAssetsAsync(assets))
                _logger.LogInformation($"userId='{userId}'|financeId='{finance.Entity.Id}'. added asset='{JsonSerializer.Serialize(asset)}'");
            await foreach (var liability in AddLiabilitiesAsync(liabilities))
                _logger.LogInformation($"userId='{userId}'|financeId='{finance.Entity.Id}'. added liability='{JsonSerializer.Serialize(liability)}'");
            await foreach (var goal in AddGoalsAsync(goals))
                _logger.LogInformation($"userId='{userId}'|financeId='{finance.Entity.Id}'. added goal='{JsonSerializer.Serialize(goal)}'");

            return finance.Entity;
        }
    }
}
