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

namespace aiof.api.services
{
    public class UserRepository : BaseRepository,
        IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly ITenant _tenant;
        private readonly AiofContext _context;
        private readonly AbstractValidator<SubscriptionDto> _subscriptionDtoValidator;
        private readonly AbstractValidator<AccountDto> _accountDtoValidator;
        private readonly AbstractValidator<UserDependentDto> _dependentDtoValidator;

        private readonly Stopwatch _sw;

        public UserRepository(
            ILogger<UserRepository> logger,
            IMapper mapper,
            AiofContext context,
            AbstractValidator<SubscriptionDto> subscriptionDtoValidator,
            AbstractValidator<AccountDto> accountDtoValidator,
            AbstractValidator<UserDependentDto> dependentDtoValidator)
            : base(logger, context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _subscriptionDtoValidator = subscriptionDtoValidator ?? throw new ArgumentNullException(nameof(subscriptionDtoValidator));
            _accountDtoValidator = accountDtoValidator ?? throw new ArgumentNullException(nameof(accountDtoValidator));
            _dependentDtoValidator = dependentDtoValidator ?? throw new ArgumentNullException(nameof(dependentDtoValidator));

            _tenant = _context.Tenant;
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
                .OrderBy(x => x.Id)
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
                ?? throw new AiofNotFoundException($"User with Id={_context.Tenant.UserId} was not found");
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
                ?? throw new AiofNotFoundException($"UserProfile for User with UserId={_context.Tenant.UserId} was not found");
        }

        public async Task<IUser> UpsertAsync(UserDto userDto)
        {
            var userInDb = await GetAsync(false) as User;
            var dtoAsUser = _mapper.Map<User>(userDto);
            var user = _mapper.Map(dtoAsUser, userInDb);

            _context.Update(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | UpsertUserAsync completed | {UserDto}",
                _tenant.Log,
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
            profile.UserId = _context.Tenant.UserId;

            _context.UserProfiles
                .Update(profile);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Upserted UserProfile with UserId={UserId}. UserProfile={UserProfile}",
                _tenant.Log,
                _context.Tenant.UserId,
                JsonSerializer.Serialize(profile));

            return profile;
        }

        #region Dependent
        public async Task<IUserDependent> GetDependentAsync(int id)
        {
            return await _context.UserDependents
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"User dependent with id={id} was not found");
        }
        public async Task<IUserDependent> GetDependentAsync(UserDependentDto dto)
        {
            return await _context.UserDependents
                .FirstOrDefaultAsync(x => x.FirstName == dto.FirstName
                    && x.LastName == dto.LastName
                    && x.Age == dto.Age
                    && x.Email == dto.Email
                    && x.AmountOfSupportProvided == dto.AmountOfSupportProvided
                    && x.UserRelationship == dto.UserRelationship);
        }
        public async Task<IEnumerable<IUserDependent>> GetDependentsAsync()
        {
            return await _context.UserDependents
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IUserDependent> AddDependentAsync(UserDependentDto userDependentDto)
        {
            await _dependentDtoValidator.ValidateAndThrowAsync(userDependentDto);

            if (await GetDependentAsync(userDependentDto) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"User dependent already exists");

            var dependent = _mapper.Map<UserDependent>(userDependentDto);

            dependent.UserId = _tenant.UserId;

            await _context.UserDependents.AddAsync(dependent);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Added UserDependent={UserDependent}",
                _tenant.Log,
                JsonSerializer.Serialize(dependent));

            return dependent;
        }

        public async Task<IUserDependent> UpdateDependentAsync(
            int userDependentId,
            UserDependentDto userDependentDto)
        {
            var userDependentInDb = await GetDependentAsync(userDependentId) as UserDependent;
            var userDependent = _mapper.Map(userDependentDto, userDependentInDb);

            _context.UserDependents
                .Update(userDependent);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated UserDependent={UserDependent}",
                _tenant.Log,
                JsonSerializer.Serialize(userDependent));

            return userDependentInDb;
        }

        public async Task DeleteDependentAsync(int id)
        {
            await base.SoftDeleteAsync<UserDependent>(id);
        }
        #endregion

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
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"Subscription with Id={id} was not found");
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
            decimal? amount,
            bool asNoTracking = true)
        {
            return await GetSubscriptionsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Name == name
                    && x.Amount == amount);
        }
        public async Task<IEnumerable<ISubscription>> GetSubscriptionsAsync(bool asNoTracking = true)
        {
            return await GetSubscriptionsQuery(asNoTracking)
                .ToListAsync();
        }

        public async Task<ISubscription> AddSubscriptionAsync(SubscriptionDto subscriptionDto)
        {
            await _subscriptionDtoValidator.ValidateAndThrowAsync(subscriptionDto);

            var name = subscriptionDto.Name;
            var amount = subscriptionDto.Amount;

            if (await GetSubscriptionAsync(name, amount) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"{nameof(Subscription)} with Name={name} and Amount={amount} already exists");

            var subscription = _mapper.Map<Subscription>(subscriptionDto);

            subscription.UserId = _tenant.UserId;

            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Created Subscription with Id={SubscriptionId}, PublicKey={SubscriptionPublicKey} and UserId={SubscriptionUserId}",
                _tenant.Log,
                subscription.Id,
                subscription.PublicKey,
                subscription.UserId);

            return subscription;
        }

        public async Task<ISubscription> UpdateSubscriptionAsync(
            int id,
            SubscriptionDto subscriptionDto)
        {
            var subscriptionInDb = await GetSubscriptionAsync(id, false);
            var subscription = _mapper.Map(subscriptionDto, subscriptionInDb as Subscription);

            _context.Subscriptions
                .Update(subscription);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated Subscription with Id={SubscriptionId}, PublicKey={SubscriptionPublicKey} and UserId={SubscriptionUserId}",
                _tenant.Log,
                subscription.Id,
                subscription.PublicKey,
                subscription.UserId);

            return subscription;
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
                .Include(x => x.Type)
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

        public async Task<IAccount> GetAccountAsync(
            int id,
            bool asNoTracking = true)
        {
            return await GetAccountsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"Account with Id={id} was not found");
        }

        public async Task<IAccount> GetAccountAsync(
            AccountDto accountDto,
            bool asNoTracking = true)
        {
            return await GetAccountsQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.Name == accountDto.Name
                    && x.Description == accountDto.Description
                    && x.TypeName == accountDto.TypeName);
        }

        public async Task<IEnumerable<IAccountType>> GetAccountTypesAsync()
        {
            return await GetAccountTypesQuery()
                .ToListAsync();
        }

        public async Task<IAccount> AddAccountAsync(AccountDto accountDto)
        {
            await _accountDtoValidator.ValidateAndThrowAsync(accountDto);

            if (await GetAccountAsync(accountDto) != null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Account already exists");

            var account = _mapper.Map<Account>(accountDto);

            account.UserId = _context.Tenant.UserId;

            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Created Account with Id={AccountId}, PublicKey={AccountPublicKey} and UserId={AccountUserId}",
                _context.Tenant.Log,
                account.Id,
                account.PublicKey,
                account.UserId);

            return account;
        }

        public async Task<IAccount> UpdateAccountAsync(
            int id,
            AccountDto accountDto)
        {
            var accountInDb = await GetAccountAsync(id, false);
            var account = _mapper.Map(accountDto, accountInDb as Account);

            _context.Accounts
                .Update(account);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Updated Account with Id={AccountId}, PublicKey={AccountPublicKey} and UserId={AccountUserId}",
                _context.Tenant.Log,
                account.Id,
                account.PublicKey,
                account.UserId);

            return account;
        }

        public async Task DeleteAccountAsync(int id)
        {
            await base.SoftDeleteAsync<Account>(id);
        }
        #endregion

        #region Utility
        public async Task<IEnumerable<IEducationLevel>> GetEducationLevelsAsync()
        {
            return await _context.EducationLevels
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IMaritalStatus>> GetMaritalStatusesAsync()
        {
            return await _context.MaritalStatuses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IResidentialStatus>> GetResidentialStatusesAsync()
        {
            return await _context.ResidentialStatuses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IGender>> GetGendersAsync()
        {
            return await _context.Genders
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IHouseholdAdult>> GetHouseholdAdultsAsync()
        {
            return await _context.HouseholdAdults
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<IHouseholdChild>> GetHouseholdChildrenAsync()
        {
            return await _context.HouseholdChildren
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IUserProfileOptions> GetUserProfileOptionsAsync()
        {
            return new UserProfileOptions
            {
                EducationLevels = await GetEducationLevelsAsync(),
                MaritalStatuses = await GetMaritalStatusesAsync(),
                ResidentialStatuses = await GetResidentialStatusesAsync(),
                Genders = await GetGendersAsync(),
                HouseholdAdults = await GetHouseholdAdultsAsync(),
                HouseholdChildren = await GetHouseholdChildrenAsync()
            };
        }
        #endregion
    }
}