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

        private IQueryable<AssetType> GetAssetTypesQuery()
        {
            return _context.AssetTypes
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

        private IQueryable<LiabilityType> GetLiabilityTypesQuery()
        {
            return _context.LiabilityTypes
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

        private IQueryable<GoalType> GetGoalTypesQuery()
        {
            return _context.GoalTypes
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

        public async Task<IEnumerable<IAssetType>> GetAssetTypesAsync()
        {
            return await GetAssetTypesQuery()
                .OrderBy(x => x.Name)
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

        public async Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync()
        {
            return await GetLiabilityTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ILiability> AddLiabilityAsync(LiabilityDto liabilityDto)
        {
            var liability = _mapper.Map<Liability>(liabilityDto);

            await _context.Liabilities
                .AddAsync(liability);

            await _context.SaveChangesAsync();

            return await GetLiabilityAsync(liability.Id);
        }

        public async IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilityDtos)
        {
            foreach (var liabilityDto in liabilityDtos)
                yield return await AddLiabilityAsync(liabilityDto);
        }


        public async Task<IGoal> GetGoalAsync(int id)
        {
            return await GetGoalsQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<IGoalType>> GetGoalTypesAsync()
        {
            return await GetGoalTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IGoal> AddGoalAsync(GoalDto goalDto)
        {
            var goal = _mapper.Map<Goal>(goalDto);

            await _context.Goals
                .AddAsync(goal);

            await _context.SaveChangesAsync();

            return await GetGoalAsync(goal.Id);
        }

        public async IAsyncEnumerable<IGoal> AddGoalsAsync(IEnumerable<GoalDto> goalDtos)
        {
            foreach (var goalDto in goalDtos)
                yield return await AddGoalAsync(goalDto);
        }


        public async Task<IFinance> GetFinanceAsync(int id)
        {
            var finance = await GetFinancesQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            return finance == null
                ? throw new AiofNotFoundException($"Finance with id='{id}' doesn't exist")
                : finance;
        }

        public async Task<IFinance> AddFinanceAsync(FinanceDto financeDto)
        {
            if (financeDto == null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"finance dto cannot be NULL");
            else if (financeDto.AssetDtos == null
                || financeDto.LiabilityDtos == null
                || financeDto.GoalDtos == null)
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"assets, liabilities and goals cannot be NULL");
            }

            //TODO check if all financeId's are the same
            var financeId = financeDto?.AssetDtos?
                .FirstOrDefault()
                .FinanceId;

            if (!financeDto.AssetDtos.All(x => x.FinanceId == financeId)
                || !financeDto.LiabilityDtos.All(x => x.FinanceId == financeId)
                || !financeDto.GoalDtos.All(x => x.FinanceId == financeId))
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"all assets, liabilities and goals must have the same 'FinanceId'");
            }

            var finance = _mapper.Map<Finance>(financeDto);

            await _context.Finances
                .AddAsync(finance);

            await _context.SaveChangesAsync();

            //await foreach (var asset in AddAssetsAsync(assetDtos))
            //    _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added asset='{JsonSerializer.Serialize(asset)}'");
            //await foreach (var liability in AddLiabilitiesAsync(liabilityDtos))
            //    _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added liability='{JsonSerializer.Serialize(liability)}'");
            //await foreach (var goal in AddGoalsAsync(goalDtos))
            //    _logger.LogInformation($"userId='{userId}'|financeId='{financeId}'. added goal='{JsonSerializer.Serialize(goal)}'");

            return await GetFinanceAsync(finance.Id);
        }
    }
}
