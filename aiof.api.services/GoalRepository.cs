using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;

using aiof.api.data;
using System.Text.Json;

namespace aiof.api.services
{
    public class GoalRepository : BaseRepository, IGoalRepository
    {
        private readonly ILogger<GoalRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<GoalDto> _goalDtoValidator;

        public GoalRepository(
            ILogger<GoalRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<GoalDto> goalDtoValidator)
            : base(logger, context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _goalDtoValidator = goalDtoValidator ?? throw new ArgumentNullException(nameof(goalDtoValidator));
        }

        private IQueryable<Goal> GetQuery(bool asNoTracking = true)
        {
            var goalsQuery = _context.Goals
                .Include(x => x.Type)
                .Include(x => x.ContributionFrequency)
                .AsQueryable();

            return asNoTracking
                ? goalsQuery.AsNoTracking()  
                : goalsQuery;
        }
        private IQueryable<GoalType> GetTypesQuery(bool asNoTracking = true)
        {
            var goalTypesQuery = _context.GoalTypes
                .AsQueryable();

            return asNoTracking
                ? goalTypesQuery.AsNoTracking()
                : goalTypesQuery;
        }

        public async Task<IGoal> GetAsync(
            int id, 
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Goal)} with Id={id} was not found");
        }
        public async Task<IGoal> GetAsync(
            Guid publicKey, 
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException($"{nameof(Goal)} with PublicKey={publicKey} was not found");
        }
        public async Task<bool> ExistsAsync(GoalDto goalDto)
        {
            return await _context.Goals
                .FirstOrDefaultAsync(x => x.Name == goalDto.Name
                    && x.TypeName == goalDto.TypeName
                    && x.Amount == goalDto.Amount
                    && x.CurrentAmount == goalDto.CurrentAmount
                    && x.Contribution == goalDto.Contribution
                    && x.ContributionFrequencyName == goalDto.ContributionFrequencyName
                    && x.UserId == goalDto.UserId) is null
                ? false
                : true;
        }

        public async Task<IEnumerable<IGoalType>> GetTypesAsync()
        {
            return await GetTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IGoal> AddAsync(GoalDto goalDto)
        {
            await _goalDtoValidator.ValidateAndThrowAsync(goalDto);

            var goal = await ExistsAsync(goalDto)
                ? throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Goal)} already exists")
                : _mapper.Map<Goal>(goalDto);

            await _context.AddAsync(goal);

            await _context.SaveChangesAsync();

            await _context.Entry(goal)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation("{Tenant} | Created Goal with Id={GoalId}, PublicKey={GoalPublicKey} and UserId={GoalUserId}",
                _context.Tenant.Log,
                goal.Id,
                goal.PublicKey,
                goal.UserId);

            return goal;
        }

        public async IAsyncEnumerable<IGoal> AddAsync(IEnumerable<GoalDto> goalDtos)
        {
            foreach (var goalDto in goalDtos)
                yield return await AddAsync(goalDto);
        }

        public async Task<IGoal> UpdateAsync(
            int id, 
            GoalDto goalDto)
        {
            if (goalDto == null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Unable to update Goal. {nameof(GoalDto)} parameter cannot be NULL");

            var goal = await GetAsync(id);

            _context.Goals
                .Update(_mapper.Map(goalDto, goal as Goal));

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Update Goal={Goal}",
                _context.Tenant.Log,
                JsonSerializer.Serialize(goal));

            return goal;
        }

        public async Task DeleteAsync(int id)
        {
            await base.SoftDeleteAsync<Goal>(id);
        }
    }
}