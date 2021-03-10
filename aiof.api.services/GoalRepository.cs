using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;
using Newtonsoft.Json;

using aiof.api.data;

namespace aiof.api.services
{
    public class GoalRepository : BaseRepository, IGoalRepository
    {
        private readonly ILogger<GoalRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<GoalDto> _goalDtoValidator;
        private readonly AbstractValidator<GoalTripDto> _goalTripDtoValidator;
        private readonly AbstractValidator<GoalHomeDto> _goalHomeDtoValidator;
        private readonly AbstractValidator<GoalCarDto> _goalCarDtoValidator;

        public GoalRepository(
            ILogger<GoalRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<GoalDto> goalDtoValidator,
            AbstractValidator<GoalTripDto> goalTripDtoValidator,
            AbstractValidator<GoalHomeDto> goalHomeDtoValidator,
            AbstractValidator<GoalCarDto> goalCarDtoValidator)
            : base(logger, context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _goalDtoValidator = goalDtoValidator ?? throw new ArgumentNullException(nameof(goalDtoValidator));
            _goalTripDtoValidator = goalTripDtoValidator ?? throw new ArgumentNullException(nameof(goalTripDtoValidator));
            _goalHomeDtoValidator = goalHomeDtoValidator ?? throw new ArgumentNullException(nameof(goalHomeDtoValidator));
            _goalCarDtoValidator = goalCarDtoValidator ?? throw new ArgumentNullException(nameof(goalCarDtoValidator));
        }

        private IQueryable<Goal> GetQuery(bool asNoTracking = true)
        {
            var goalsQuery = _context.Goals
                .AsQueryable();

            return asNoTracking
                ? goalsQuery.AsNoTracking()  
                : goalsQuery;
        }

        public async Task<IGoal> GetAsync(
            int id, 
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"Goal with Id={id} was not found");
        }

        public async Task<IGoal> GetAsync(
            GoalDto dto,
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Name == dto.Name
                    && x.Type == dto.Type
                    && x.Amount == dto.Amount
                    && x.CurrentAmount == dto.CurrentAmount
                    && x.MonthlyContribution == dto.MonthlyContribution
                    && x.PlannedDate == dto.PlannedDate
                    && x.ProjectedDate == dto.ProjectedDate);
        }

        public async Task<IEnumerable<IGoal>> GetAsync(
            GoalType type,
            bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .Where(x => x.Type == type)
                .OrderBy(x => x.Type)
                .ToListAsync();
        }

        public async Task<IEnumerable<IGoal>> GetAllAsync(bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .ToListAsync();
        }
        public async Task<IEnumerable<object>> GetAllAsObjectsAsync(bool asNoTracking = true)
        {
            var goals = new List<object>();
            var goalsInDb = await GetQuery(asNoTracking)
                .ToListAsync();

            foreach (var goal in goalsInDb)
                goals.Add(goal);

            return goals;
        }

        public async Task<IGoal> AddAsync(string dtoStr)
        {
            if (string.IsNullOrWhiteSpace(dtoStr))
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Error while adding Goal. Payload cannot be empty");

            var dto = JsonConvert.DeserializeObject<GoalDto>(dtoStr);
            IGoal goal;

            // Validate and throw
            await _goalDtoValidator.ValidateAndThrowAsync(dto);

            // Based on Type, perform the appropriate actions
            if (dto.Type == GoalType.Generic)
            {
                goal = await AddAsync(dto);
            }
            else if (dto.Type == GoalType.Trip)
            {
                goal = await AddAsync(JsonConvert.DeserializeObject<GoalTripDto>(dtoStr));  
            }
            else if (dto.Type == GoalType.BuyAHome)
            {
                goal = await AddAsync(JsonConvert.DeserializeObject<GoalHomeDto>(dtoStr));
            }
            else if (dto.Type == GoalType.BuyACar)
            {
                goal = await AddAsync(JsonConvert.DeserializeObject<GoalCarDto>(dtoStr));
            }
            else
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Error while adding Goal. {dto.Type} Type is not yet supported");
            }

            _logger.LogInformation("{Tenant} | Added {GoalType} Goal with Id={GoalId}, PublicKey={GoalPublicKey} and UserId={GoalUserId}",
                _context.Tenant.Log,
                goal.Type,
                goal.Id,
                goal.PublicKey,
                goal.UserId);

            return goal;
        }

        public async Task<IGoal> AddAsync(GoalDto dto)
        {
            var goal = _mapper.Map<Goal>(dto);

            goal.UserId = _context.Tenant.UserId;

            await _context.Goals.AddAsync(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<IGoal> AddAsync(GoalTripDto dto)
        {
            // Validate and throw
            await _goalTripDtoValidator.ValidateAndThrowAsync(dto);

            var goal = _mapper.Map<GoalTrip>(dto);

            goal.UserId = _context.Tenant.UserId;

            // Calculate the amount, if it's null
            goal.Amount = goal.Amount ??
                (goal.Flight ?? 0
                + goal.Hotel ?? 0
                + goal.Car ?? 0
                + goal.Food ?? 0
                + goal.Activities ?? 0
                + goal.Other) * goal.Travelers;

            await _context.GoalsTrip.AddAsync(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<IGoal> AddAsync(GoalHomeDto dto)
        {
            // Validate and throw
            await _goalHomeDtoValidator.ValidateAndThrowAsync(dto);

            var goal = _mapper.Map<GoalHome>(dto);

            goal.UserId = _context.Tenant.UserId;

            // Calculate the amount and recommended amount. Counting insurance, property tax, closing costs, etc.
            goal.Amount = goal.Amount ?? goal.HomeValue * goal.PercentDownPayment;
            goal.RecommendedAmount = goal.RecommendedAmount ?? goal.Amount + goal.HomeValue * 0.01M;

            await _context.GoalsHome.AddAsync(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<IGoal> AddAsync(GoalCarDto dto)
        {
            // Validate and throw
            await _goalCarDtoValidator.ValidateAndThrowAsync(dto);

            var goal = _mapper.Map<GoalCar>(dto);

            goal.UserId = _context.Tenant.UserId;

            // Calculate the amount if not given
            goal.Amount = goal.Amount ?? 
                goal.Price - (goal.DesiredMonthlyPayment * goal.LoanTermMonths);

            await _context.GoalsCar.AddAsync(goal);
            await _context.SaveChangesAsync();

            return goal;
        }

        public async Task<IGoal> UpdateAsync(
            int id, 
            GoalDto dto)
        {
            await CheckAsync(dto);

            var goal = await GetAsync(id, false);
            var goalToUpdate = _mapper.Map(dto, goal as Goal);

            _context.Goals
                .Update(goalToUpdate);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated Goal with Id={GoalId}, PublicKey={GoalPublicKey} and UserId={GoalUserId}",
                _context.Tenant.Log,
                goal.Id,
                goal.PublicKey,
                goal.UserId);

            return goal;
        }

        public async Task DeleteAsync(int id)
        {
            await base.SoftDeleteAsync<Goal>(id);
        }

        private async Task CheckAsync(
            GoalDto dto,
            string message = null)
        {
            if (dto == null)
            { throw new AiofFriendlyException(HttpStatusCode.BadRequest, message ?? $"Goal DTO cannot be NULL"); }
            else if (await GetAsync(dto) != null) 
            { throw new AiofFriendlyException(HttpStatusCode.BadRequest, message ?? $"Goal already exists"); }
        }
    }
}