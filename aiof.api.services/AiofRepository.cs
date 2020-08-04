using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;
using FluentValidation;

using aiof.api.data;

namespace aiof.api.services
{
    public class AiofRepository : IAiofRepository
    {
        private readonly ILogger<AiofRepository> _logger;
        private readonly IMapper _mapper;
        private readonly AiofContext _context;

        public AiofRepository(
            ILogger<AiofRepository> logger, 
            IMapper mapper, 
            AiofContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IQueryable<User> GetUsersQuery()
        {
            return _context.Users
                .Include(x => x.Assets)
                .Include(x => x.Goals)
                .Include(x => x.Liabilities)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<IUser> GetUserAsync(int id)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IUser> GetUserAsync(string username)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<IUser> AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            await _context.Users
                .AddAsync(user);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"added user='{JsonSerializer.Serialize(user)}'");

            return user;
        }

        public async Task<bool> IsUserUniqueAsync(string username, string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username
                    || x.Email == email);
            
            return user == null
                ? true
                : false;
        }
    }
}
