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
    public abstract class BaseRepository
    {
        private readonly AiofContext _context;
        private readonly ILogger<BaseRepository> _logger;

        public BaseRepository(
            ILogger<BaseRepository> logger,
            AiofContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IQueryable<T> GetEntityQuery<T>(bool asNoTracking = true)
            where T : class, IPublicKeyId
        {
            return asNoTracking
                ? _context.Set<T>()
                    .AsNoTracking()
                    .AsQueryable()
                : _context.Set<T>()
                    .AsQueryable();
        }

        public async Task<T> GetEntityAsync<T>(int id)
            where T : class, IPublicKeyId
        {
            return await GetEntityQuery<T>()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with Id='{id}' was not found");
        }

        public async Task<T> GetEntityAsync<T>(Guid publicKey)
            where T : class, IPublicKeyId
        {
            return await GetEntityQuery<T>()
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with PublicKey='{publicKey}' was not found");
        }

        public async Task<T> GetEntityAsync<T>(string publicKey)
            where T : class, IPublicKeyId
        {
            return await GetEntityAsync<T>(Guid.Parse(publicKey));
        }

        public async Task DeleteAsync<T>(int id)
            where T : class, IPublicKeyId
        {
            var entity = await GetEntityQuery<T>()
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted {typeof(T).Name}='{JsonSerializer.Serialize(entity)}'");
        }
        public async Task DeleteAsync<T>(T entity)
            where T : class, IPublicKeyId
        {
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Deleted {typeof(T).Name}='{JsonSerializer.Serialize(entity)}'");
        }
    }
}