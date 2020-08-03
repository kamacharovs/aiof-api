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

        private IQueryable<Liability> GetLiabilitiesQuery()
        {
            return _context.Liabilities
                .Include(x => x.Type)
                .AsNoTracking()
                .AsQueryable();
        }

        private IQueryable<LiabilityType> GetLiabilityTypesQuery()
        {
            return _context.LiabilityTypes
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

        public async Task<ILiability> GetLiabilityAsync(int id)
        {
            return await GetLiabilitiesQuery()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync()
        {
            return await GetLiabilityTypesQuery()
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<ILiability> AddLiabilityAsync(LiabilityDto liabilityDto)
        {
            var liability = _mapper.Map<Liability>(liabilityDto);

            await _context.Liabilities
                .AddAsync(liability);

            await _context.SaveChangesAsync();

            return liability;
        }

        public async IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilityDtos)
        {
            foreach (var liabilityDto in liabilityDtos)
                yield return await AddLiabilityAsync(liabilityDto);
        }

        public async Task<ILiability> UpdateLiabilityAsync(int id, LiabilityDto liabilityDto)
        {
            if (liabilityDto == null)
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Unable to update 'Liability'. '{nameof(LiabilityDto)}' parameter cannot be NULL");

            var liability = await GetLiabilityAsync(id);

            if (liability == null)
                throw new AiofNotFoundException($"Unable to find 'Liability' with id='{id}'");

            _context.Liabilities
                .Update(_mapper.Map(liabilityDto, liability as Liability));

            await _context.SaveChangesAsync();

            return liability;
        }
    }
}
