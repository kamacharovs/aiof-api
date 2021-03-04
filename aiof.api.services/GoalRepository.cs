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
            Goal goal;

            if (dto.Type == GoalType.Generic)
            {
                goal = _mapper.Map<Goal>(dto);
                goal.UserId = _context.Tenant.UserId;

                await _context.Goals.AddAsync(goal);
                await _context.SaveChangesAsync();
            }
            else if (dto.Type == GoalType.Trip)
            {
                goal = _mapper.Map<GoalTrip>(JsonConvert.DeserializeObject<GoalTripDto>(dtoStr));
                goal.UserId = _context.Tenant.UserId;

                // Calculate the amount, if it's null
                var goalTrip = goal as GoalTrip;

                goalTrip.Amount = goal.Amount ?? 
                    (goalTrip.Flight ?? 0
                    + goalTrip.Hotel ?? 0
                    + goalTrip.Car ?? 0
                    + goalTrip.Food ?? 0
                    + goalTrip.Activities ?? 0
                    + goalTrip.Other) * goalTrip.Travelers;

                await _context.GoalsTrip.AddAsync(goalTrip);
                await _context.SaveChangesAsync();
            }
            else if (dto.Type == GoalType.BuyAHome)
            {
                goal = _mapper.Map<GoalHome>(JsonConvert.DeserializeObject<GoalHomeDto>(dtoStr));
                goal.UserId = _context.Tenant.UserId;

                // Calculate the amount, if it's null
                var goalHome = goal as GoalHome;

                goalHome.Amount = goalHome.Amount ??
                    goalHome.HomeValue * goalHome.PercentDownPayment;

                await _context.GoalsHome.AddAsync(goalHome);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Error while adding Goal. {dto.Type} Type is not yet supported");
            }

            _logger.LogInformation("{Tenant} | Created {GoalType} Goal with Id={GoalId}, PublicKey={GoalPublicKey} and UserId={GoalUserId}",
                _context.Tenant.Log,
                goal.Type,
                goal.Id,
                goal.PublicKey,
                goal.UserId);

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