using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using AutoMapper;

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

        private IQueryable<User> GetUsersQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Users
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Users
                    .Include(x => x.Assets)
                    .Include(x => x.Goals)
                    .Include(x => x.Liabilities)
                    .AsQueryable();
        }
        private IQueryable<User> GetUsersBaseQuery(bool asNoTracking = true)
        {
            return asNoTracking
                ? _context.Users
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Users
                    .AsQueryable();
        }

        public async Task<IUser> GetUserAsync(
            int id,
            bool included = true,
            bool asNoTracking = true)
        {
            return included
                ? await GetUsersQuery(asNoTracking)
                    .FirstOrDefaultAsync(x => x.Id == id)
                : await GetUsersBaseQuery(asNoTracking)
                    .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{nameof(User)} with Id='{id}' was not found");
        }

        public async Task<IUser> GetUserAsync(string username)
        {
            return await GetUsersQuery()
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<IUser> UpsertFinanceAsync(
            int userId,
            UserDto userDto)
        {
            var user = _mapper.Map(userDto, await GetUserAsync(userId) as User);

            _context.Update(user);

            await _context.SaveChangesAsync();

            return await GetUserAsync(userId);
        }
    }
}
