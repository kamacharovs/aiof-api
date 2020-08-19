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

        public async Task<UserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true)
        {
            return await GetUserProfilesQuery(asNoTracking)
                .FirstOrDefaultAsync(x => x.User.Username == username);
        }


    }
}