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

        public IQueryable<T> GetQuery<T>(bool asNoTracking = true)
            where T : class, IPublicKeyId
        {
            var query = _context.Set<T>()
                .AsQueryable();

            return asNoTracking
                ? query.AsNoTracking()
                : query;
        }

        public async Task<T> GetAsync<T>(
            int id,
            bool asNoTracking = true)
            where T : class, IPublicKeyId
        {
            return await GetQuery<T>(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with Id='{id}' was not found");
        }

        public async Task<T> GetAsync<T>(
            Guid publicKey,
            bool asNoTracking = true)
            where T : class, IPublicKeyId
        {
            return await GetQuery<T>(asNoTracking)
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with PublicKey='{publicKey}' was not found");
        }

        public async Task<T> GetEntityAsync<T>(string publicKey)
            where T : class, IPublicKeyId
        {
            return await GetAsync<T>(Guid.Parse(publicKey));
        }

        public async Task SoftDeleteAsync<T>(int id)
            where T : class, IPublicKeyId, IIsDeleted
        {
            var query = _context.Set<T>();
            var entity = await query
                .FirstOrDefaultAsync(x => x.Id == id);

            entity.IsDeleted = true;
            query.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync<T>(int id)
            where T : class, IPublicKeyId
        {
            await DeleteAsync(await GetAsync<T>(id, false));
        }
        public async Task DeleteAsync<T>(Guid publicKey)
            where T : class, IPublicKeyId
        {
            await DeleteAsync(await GetAsync<T>(publicKey, false));
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