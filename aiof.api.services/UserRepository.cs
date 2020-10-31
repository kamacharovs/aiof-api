using System;
using System.Text.Json;
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
using Microsoft.EntityFrameworkCore.Internal;

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

        private IQueryable<User> GetQuery(bool asNoTracking = true)
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
        private IQueryable<UserProfile> GetProfilesQuery(bool asNoTracking = true)
        {
            var usersProfileQuery = _context.UserProfiles
                .Include(x => x.User)
                .AsQueryable();

            return asNoTracking
                ? usersProfileQuery.AsNoTracking()
                : usersProfileQuery;
        }

        public async Task<IUser> GetAsync(bool asNoTracking = true)
        {
            return await GetQuery(asNoTracking)
                .FirstOrDefaultAsync()
                ?? throw new AiofNotFoundException($"User with Id={_context._tenant.UserId} was not found");
        }
        public async Task<IUser> GetAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetQuery()
                .FirstOrDefaultAsync(x => x.Username == username)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Username={username} was not found");
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Users
                .AnyAsync(x => x.Id == id);
        }

        public async Task<IUserProfile> GetProfileAsync(bool asNoTracking = true)
        {
            return await GetProfilesQuery(asNoTracking)
                .FirstOrDefaultAsync()
                ?? throw new AiofNotFoundException($"UserProfile for User with UserId={_context._tenant.UserId} was not found");
        }

        public async Task<IUser> UpsertAsync(UserDto userDto)
        {
            var userInDb = await GetAsync(false) as User;
            var dtoAsUser = _mapper.Map<User>(userDto);
            var user = _mapper.Map(dtoAsUser, userInDb);

            _context.Update(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | UpsertUserAsync completed | {UserDto}",
                _tenant,
                userDto.ToString());

            return await GetAsync();
        }

        public async Task<IUserProfile> UpsertProfileAsync(UserProfileDto userProfileDto)
        {
            var profile = new UserProfile();
            try
            {
                profile = await GetProfileAsync(false) as UserProfile;
            }
            catch (Exception e) when (e is AiofNotFoundException) { }

            profile = _mapper.Map(userProfileDto, profile);
            profile.UserId = _context._tenant.UserId;

            _context.UserProfiles
                .Update(profile);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Upserted UserProfile with UserId={UserId}. UserProfile={UserProfile}",
                _tenant,
                _context._tenant.UserId,
                JsonSerializer.Serialize(profile));

            return profile;
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

        public async Task<ISubscription> GetSubscriptionAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetSubscriptionsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<ISubscription> GetSubscriptionAsync(
            Guid publicKey,
            bool asNoTracking = true)
        {
            return await GetSubscriptionsQuery(asNoTracking)
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
            var subscriptionInDb = await GetSubscriptionAsync(id, false);
            var subcription = _mapper.Map(subscriptionDto, subscriptionInDb as Subscription);
            
            _context.Subscriptions
                .Update(subcription);

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
        private IQueryable<AccountType> GetAccountTypesQuery(bool asNoTracking = true)
        {
            var query = _context.AccountTypes
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }
        private IQueryable<AccountTypeMap> GetAccountTypeMapsQuery(bool asNoTracking = true)
        {
            var query = _context.AccountTypeMaps
                .Include(x => x.AccountType)
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }

        public async Task<IAccount> GetAccountAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetAccountsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(Account)} with Id={id} was not found");
        }

        public async Task<IAccount> GetAccountAsync(
            AccountDto accountDto,
            bool asNoTracking = true)
        {
            return await GetAccountsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Name == accountDto.Name
                    && x.Description == accountDto.Description
                    && x.TypeName == accountDto.TypeName
                    && x.UserId == accountDto.UserId);
        }

        public async Task<IEnumerable<IAccountType>> GetAccountTypesAsync()
        {
            return await GetAccountTypesQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<IAccountTypeMap>> GetAccountTypeMapsAsync()
        {
            return await GetAccountTypeMapsQuery()
                .ToListAsync();
        }

        public async Task<IAccount> AddAccountAsync(AccountDto accountDto)
        {
            var accountInDb = await GetAccountAsync(accountDto);
            if (accountInDb != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Account with Name={accountDto.Name}, Description={accountDto.Description}, Type={accountDto.TypeName} already exists");

            var account = _mapper.Map<Account>(accountDto);

            await _context.Accounts
                .AddAsync(account);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Added Account={Account}",
                _tenant,
                JsonSerializer.Serialize(account));

            return account;
        }

        public async Task<IAccount> UpdateAccountAsync(
            int id,
            AccountDto accountDto)
        {
            var accountInDb = await GetAccountAsync(id, false) as Account;
            var account = _mapper.Map(accountDto, accountInDb);

            _context.Accounts
                .Update(account);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated Account={Account}",
                _tenant,
                JsonSerializer.Serialize(account));

            return account;
        }

        public async Task DeleteAccountAsync(int id)
        {
            await base.SoftDeleteAsync<Account>(id);
        }
        #endregion
    }
}