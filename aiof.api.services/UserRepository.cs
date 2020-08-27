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
    public class UserRepository : BaseRepository,
        IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;
        private readonly AbstractValidator<SubscriptionDto> _subscriptionDtoValidator;

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
        public async Task<ISubscription> GetSubscriptionAsync(Guid publicKey)
        {
            return await base.GetAsync<Subscription>(publicKey);
        }

        public async Task<ISubscription> AddSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            await _subscriptionDtoValidator.ValidateAndThrowAsync(subscriptionDto);

            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            await _context.Subscriptions
                .AddAsync(subscription);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Added {nameof(Subscription)}='{JsonSerializer.Serialize(subscription)}'");

            return subscription;
        }
        #endregion
    }
}