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

using AutoMapper;

using aiof.api.data;

namespace aiof.api.services
{
    public class AiofRepository : IAiofRepository
    {
        private readonly AiofContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AiofRepository> _logger;

        public AiofRepository(AiofContext context, IMapper mapper, ILogger<AiofRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public async Task<IAsset> AddAssetAsync(AssetDto assetDto)
        {
            var asset = _mapper.Map<Asset>(assetDto);

            await _context.Assets
                .AddAsync(asset);

            await _context.SaveChangesAsync();

            return await GetAssetAsync(asset.Id);
        }

        public async IAsyncEnumerable<IAsset> AddAssetsAsync(IEnumerable<AssetDto> assetsDto)
        {
            foreach (var assetDto in assetsDto)
                yield return await AddAssetAsync(assetDto);
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
            IEnumerable<AssetDto> assetDtos,
            IEnumerable<Liability> liabilities,
            IEnumerable<Goal> goals)
        {
            if (assetDtos == null
                || liabilities == null
                || goals == null)
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"assets, liabilities and goals cannot be NULL");
            }

            //TODO check if all financeId's are the same
            var financeId = assetDtos?.FirstOrDefault()
                .FinanceId;

            if (!assetDtos.All(x => x.FinanceId == financeId)
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

            await foreach (var asset in AddAssetsAsync(assetDtos))
                _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added asset='{JsonSerializer.Serialize(asset)}'");
            await foreach (var liability in AddLiabilitiesAsync(liabilities))
                _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added liability='{JsonSerializer.Serialize(liability)}'");
            await foreach (var goal in AddGoalsAsync(goals))
                _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added goal='{JsonSerializer.Serialize(goal)}'");

            return finance.Entity;
        }
    }
}
