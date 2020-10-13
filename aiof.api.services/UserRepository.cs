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

        private readonly string _tenant;
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

            _tenant = _context._tenant.Log;
            _sw = new Stopwatch();
        }

        private IQueryable<User> GetUsersQuery(bool asNoTracking = true)
        {
            var usersQuery = _context.Users
                .Include(x => x.Profile)
                .Include(x => x.Assets)
                .Include(x => x.Goals)
                .Include(x => x.Liabilities)
                .Include(x => x.Subscriptions)
                .Include(x => x.Accounts)
                .AsQueryable();

            return asNoTracking
                ? usersQuery.AsNoTracking()
                : usersQuery;
        }

        private IQueryable<UserProfile> GetUserProfilesQuery(bool asNoTracking = true)
        {
            var usersProfileQuery = _context.UserProfiles
                .Include(x => x.User)
                .AsQueryable();

            return asNoTracking
                ? usersProfileQuery.AsNoTracking()
                : usersProfileQuery;
        }

        public async Task<IUser> GetAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetUsersQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Id={id} was not found");
        }
        public async Task<IUser> GetUserAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Username == username)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Username={username} was not found");
        }

        public async Task<IUserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetUserProfilesQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.User.Username == username)
                ?? throw new AiofNotFoundException($"{nameof(UserProfile)} for {nameof(User)} with Username={username} was not found");
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
            _logger.LogInformation("UpsertFinanceAsync algorithm took {AlgorithmTime} (µs)",
                _sw.Elapsed.TotalMilliseconds * 1000);

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

            _logger.LogInformation("{Tenant} | Upserted User with Username={Username}. UserProfile={UserProfile}",
                _tenant,
                username,
                JsonSerializer.Serialize(user.Profile));

            return await GetUserAsync(username);
        }

        #region Subscription
        private IQueryable<Subscription> GetSubscriptionsQuery(bool asNoTracking = true)
        {
            var subscriptionQuery = _context.Subscriptions
                .Include(x => x.PaymentFrequency)
                .AsQueryable();

            return asNoTracking
                ? subscriptionQuery.AsNoTracking()
                : subscriptionQuery;
        }

        public async Task<ISubscription> GetSubscriptionAsync(int id)
        {
            return await GetSubscriptionsQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ISubscription> GetSubscriptionAsync(Guid publicKey)
        {
            return await GetSubscriptionsQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey);
        }
        public async Task<ISubscription> GetSubscriptionAsync(
            string name, 
            decimal amount,
            int userId,
            bool asNoTracking = true)
        {
            return await GetSubscriptionsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.UserId == userId
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
                    $"{nameof(Subscription)} with UserId={userId}, Name='{name}' and Amount={amount} already exists");

            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            await _context.Subscriptions
                .AddAsync(subscription);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Added Subscription={Subscription}",
                _tenant,
                JsonSerializer.Serialize(subscription));

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

            _logger.LogInformation("{Tenant} | Updated Subscription={Subscription}",
                _tenant,
                JsonSerializer.Serialize(subcription));

            return subcription;
        }

        public async Task DeleteSubscriptionAsync(int id)
        {
            await base.SoftDeleteAsync<Subscription>(id);
        }
        #endregion


        #region Account
        private IQueryable<Account> GetAccountsQuery(bool asNoTracking = true)
        {
            var query = _context.Accounts
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }
        private IQueryable<AccountType> GetAccountTypes(bool asNoTracking = true)
        {
            var query = _context.AccountTypes
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }

        public async Task<IAccount> GetAccountAsync(int id)
        {
            return await GetAccountsQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Account)} with Id={id} was not found");
        }

        public async Task<IEnumerable<IAccountType>> GetAccountTypesAsync()
        {
            return await GetAccountTypes()
                .ToListAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            await base.SoftDeleteAsync<Account>(id);
        }
        #endregion
    }
}