using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;

using aiof.api.data;
using System.Text.Json;

namespace aiof.api.services
{
    public class UserRepository : BaseRepository,
        IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<SubscriptionDto> _subscriptionDtoValidator;

        private readonly Stopwatch _sw;

        public UserRepository(
            ILogger<UserRepository> logger,
            IMapper mapper, 
            AiofContext context,
            AbstractValidator<SubscriptionDto> subscriptionDtoValidator)
            : base(logger, context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _subscriptionDtoValidator = subscriptionDtoValidator ?? throw new ArgumentNullException(nameof(subscriptionDtoValidator));

            _sw = new Stopwatch();
        }

        private IQueryable<User> GetUsersQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Users
                    .Include(x => x.Profile)
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
                    .Include(x => x.Subscriptions)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Users
                    .Include(x => x.Profile)
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
                    .Include(x => x.Subscriptions)
                    .AsQueryable();
        }

        private IQueryable<UserProfile> GetUserProfilesQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.UserProfiles
                    .Include(x => x.User)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.UserProfiles
                    .Include(x => x.User)
                    .AsQueryable();
        }

        public async Task<IUser> GetAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetUsersQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Id='{id}' was not found");
        }
        public async Task<IUser> GetUserAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Username == username)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Username='{username}' was not found");
        }

        public async Task<IUserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetUserProfilesQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.User.Username == username)
                ?? throw new AiofNotFoundException($"{nameof(UserProfile)} for {nameof(User)} with Username='{username}' was not found");
        }

        public async Task<IUser> UpsertFinanceAsync(
            int userId,
            UserDto userDto)
        {
            var userInDb = await GetAsync(userId) as User;
            var userDtoMapped = _mapper.Map<User>(userDto);

            _sw.Start();
            var user = _mapper.Map(userInDb, userDtoMapped);           
            _sw.Stop();      
            _logger.LogInformation($"UpsertFinanceAsync algorithm took {_sw.Elapsed.TotalMilliseconds * 1000} (µs)");

            _context.Update(user);
            await _context.SaveChangesAsync();

            return await GetAsync(userId);
        }

        public async Task<IUser> UpsertUserProfileAsync(
            string username, 
            UserProfileDto userProfileDto)
        {
            var user = await GetUserAsync(username) as User;

            user.Profile = _mapper.Map(userProfileDto, user.Profile);
            user.Profile.UserId = user.Id;
            user.Profile.Age = user.Profile.DateOfBirth is null ? null : (int?)(DateTime.UtcNow.Year - user.Profile.DateOfBirth.Value.Year);

            _context.Users
                .Update(user);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Upserted {nameof(User)} with {nameof(User.Username)}='{username}' " +
                $"{nameof(UserProfile)}='{JsonSerializer.Serialize(user.Profile)}'");

            return await GetUserAsync(username);
        }

        #region Subscription
        public async Task<ISubscription> GetSubscriptionAsync(int id)
        {
            return await base.GetAsync<Subscription>(id);
        }
        public async Task<ISubscription> GetSubscriptionAsync(Guid publicKey)
        {
            return await base.GetAsync<Subscription>(publicKey);
        }
        public async Task<ISubscription> GetSubscriptionAsync(
            string name, 
            decimal amount,
            int userId,
            bool asNoTracking = true)
        {
            var subscriptions = asNoTracking
                ? _context.Subscriptions.AsNoTracking().AsQueryable()
                : _context.Subscriptions.AsQueryable();

            return await subscriptions.FirstOrDefaultAsync(x => x.UserId == userId
                && x.Name == name
                && x.Amount == amount);
        }

        public async Task<ISubscription> AddSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            await _subscriptionDtoValidator.ValidateAndThrowAsync(subscriptionDto);

            var name = subscriptionDto.Name;
            var amount = subscriptionDto.Amount;
            var userId = subscriptionDto.UserId;

            if (await GetSubscriptionAsync(name, amount, userId) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Subscription)} with UserId='{userId}', Name='{name}' and Amount='{amount}' already exists");

            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            await _context.Subscriptions
                .AddAsync(subscription);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Added {nameof(Subscription)}='{JsonSerializer.Serialize(subscription)}'");

            return subscription;
        }

        public async Task<ISubscription> UpdateSubscriptionAsync(
            int id,
            SubscriptionDto subscriptionDto)
        {
            var subscriptionInDb = await GetSubscriptionAsync(id);
            var mappedDto = _mapper.Map<Subscription>(subscriptionDto);
            var subcription = _mapper.Map(subscriptionInDb as Subscription, mappedDto);
            
            _context.Subscriptions.Update(subcription);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Updated {nameof(Subscription)}='{JsonSerializer.Serialize(subcription)}'");

            return subcription;
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            await base.DeleteAsync<Subscription>(id);
        }
        #endregion
    }
}