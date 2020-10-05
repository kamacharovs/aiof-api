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

        private IQueryable<Goal> GetGoalsQuery(bool asNoTracking = true)
        {
            var goalsQuery = _context.Goals
                .Include(x => x.Type)
                .Include(x => x.ContributionFrequency)
                .AsQueryable();

            return asNoTracking
                ? goalsQuery.AsNoTracking()  
                : goalsQuery;
        }

        private IQueryable<GoalType> GetGoalTypesQuery(bool asNoTracking = true)
        {
            var goalTypesQuery = _context.GoalTypes
                .AsQueryable();

            return asNoTracking
                ? goalTypesQuery.AsNoTracking()
                : goalTypesQuery;
        }

        public async Task<IGoal> GetGoalAsync(int id)
        {
            return await GetGoalsQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Goal)} with Id={id} was not found");
        }
        public async Task<IGoal> GetAsync(
            Guid publicKey, 
            bool asNoTracking = true)
        {
            return await GetGoalsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException($"{nameof(Goal)} with PublicKey={publicKey} was not found");
        }
        public async Task<bool> GoalExistsAsync(GoalDto goalDto)
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

        public async Task<IEnumerable<IGoalType>> GetGoalTypesAsync()
        {
            return await GetGoalTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IGoal> AddGoalAsync(GoalDto goalDto)
        {
            await _goalDtoValidator.ValidateAndThrowAsync(goalDto);

            var goal = await GoalExistsAsync(goalDto)
                ? throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Goal)} already exists")
                : _mapper.Map<Goal>(goalDto);

            await _context.AddAsync(goal);

            await _context.SaveChangesAsync();

            await _context.Entry(goal)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation("Created Goal with Id={GoalId}, PublicKey={GoalPublicKey} and UserId={GoalUserId}",
                goal.Id,
                goal.PublicKey,
                goal.UserId);

            return goal;
        }

        public async IAsyncEnumerable<IGoal> AddGoalsAsync(IEnumerable<GoalDto> goalDtos)
        {
            foreach (var goalDto in goalDtos)
                yield return await AddGoalAsync(goalDto);
        }

        public async Task<IGoal> UpdateGoalAsync(
            int id, 
            GoalDto goalDto)
        {
            if (goalDto == null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Unable to update Goal. {nameof(GoalDto)} parameter cannot be NULL");

            var goal = await GetGoalAsync(id);

            _context.Goals
                .Update(_mapper.Map(goalDto, goal as Goal));

            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task DeleteAsync(Guid publicKey)
        {
            var goal = await GetAsync(publicKey, false);
            await base.DeleteAsync(goal as Goal);
        }
        public async Task DeleteAsync(IGoal goal)
        {
            await base.DeleteAsync<Goal>(goal as Goal);
        }
    }
}