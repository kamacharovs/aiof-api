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

using aiof.api.data;

namespace aiof.api.services
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IPublicKeyId
    {
        private readonly AiofContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(AiofContext context, IMapper mapper, ILogger<BaseRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<T> GetEntityQuery()
        {
            return _context.Set<T>()
                .AsNoTracking();
        }

        public async Task<T> GetEntityAsync(int id)
        {
            return await GetEntityQuery()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException();
        }

        public async Task<T> GetEntityAsync(Guid publicKey)
        {
            return await GetEntityQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException();
        }

        public async Task<T> GetEntityAsync(string publicKey)
        {
            return await GetEntityQuery()
                .FirstOrDefaultAsync(x => x.PublicKey == Guid.Parse(publicKey))
                ?? throw new AiofNotFoundException();
        }
    }
}