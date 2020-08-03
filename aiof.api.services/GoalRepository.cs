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
    public class GoalRepository : IGoalRepository
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
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _goalDtoValidator = goalDtoValidator ?? throw new ArgumentNullException(nameof(goalDtoValidator));
        }

        private IQueryable<Goal> GetGoalsQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Goals
                    .Include(x => x.Type)
                    .Include(x => x.User)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Goals
                    .Include(x => x.Type)
                    .Include(x => x.User)
                    .AsQueryable();
        }

        private IQueryable<GoalType> GetGoalTypesQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.GoalTypes
                    .AsNoTracking()
                    .AsQueryable()
                : _context.GoalTypes
                    .AsQueryable();
        }

        public async Task<IGoal> GetGoalAsync(int id)
        {
            return await GetGoalsQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Goal)} with Id='{id}' was not found");
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

            var goal = _mapper.Map<Goal>(goalDto);

            await _context.Goals
                .AddAsync(goal);

            await _context.SaveChangesAsync();

            await _context.Entry(goal)
                .Reference(x => x.Type)
                .LoadAsync();

            _logger.LogInformation($"Created {nameof(Goal)} with Id='{goal.Id}', PublicKey='{goal.PublicKey}' and UserId='{goal.UserId}'");

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
                    $"Unable to update 'Goal'. '{nameof(GoalDto)}' parameter cannot be NULL");

            var goal = await GetGoalAsync(id);

            _context.Goals
                .Update(_mapper.Map(goalDto, goal as Goal));

            await _context.SaveChangesAsync();

            return goal;
        }
    }
}