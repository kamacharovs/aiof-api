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
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;

        public UserRepository(
            ILogger<UserRepository> logger,
            IMapper mapper, 
            AiofContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IQueryable<User> GetUsersQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Users
                    .Include(x => x.Profile)
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Users
                    .Include(x => x.Profile)
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
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

        public async Task<IUserProfile> UpdateUserProfileAsync(
            string username,
            UserProfileDto userProfileDto)
        {
            var userProfileInDb = await GetUserProfileAsync(
                username, 
                asNoTracking: false) as UserProfile;

            var userProfile = _mapper.Map(userProfileDto, userProfileInDb);

            _context.UserProfiles
                .Update(userProfile);

            await _context.SaveChangesAsync();

            return userProfile;
        }
    }
}