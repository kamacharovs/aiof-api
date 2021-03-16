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
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with Id={id} was not found");
        }

        public async Task<T> GetAsync<T>(
            Guid publicKey,
            bool asNoTracking = true)
            where T : class, IPublicKeyId
        {
            return await GetQuery<T>(asNoTracking)
                .FirstOrDefaultAsync(x => x.PublicKey == publicKey)
                ?? throw new AiofNotFoundException($"{typeof(T).Name} with PublicKey={publicKey} was not found");
        }

        public async Task<T> GetEntityAsync<T>(string publicKey)
            where T : class, IPublicKeyId
        {
            return await GetAsync<T>(Guid.Parse(publicKey));
        }

        public async Task<T> QuickAddAsync<T>(T entity)
            where T : class, IPublicKeyId
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Added {EntityName} with Id={EntityId} and PublicKey={EntityPublicKey}",
                _context.Tenant.Log,
                typeof(T).Name,
                entity.Id,
                entity.PublicKey);

            return entity;
        }

        public async Task SoftDeleteAsync<T>(int id)
            where T : class, IPublicKeyId, IIsDeleted
        {
            var query = _context.Set<T>();
            var entity = await GetAsync<T>(id, false);

            entity.IsDeleted = true;
            query.Update(entity);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{Tenant} | Soft Deleted {EntityName} with Id={EntityId}",
                _context.Tenant.Log,
                typeof(T).Name,
                id);
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
            _logger.LogInformation("{Tenant} | Deleted {EntityName}={Entity}",
                _context.Tenant.Log,
                typeof(T).Name,
                JsonSerializer.Serialize(entity));
        }

        public double FutureValue(
            double rate,
            double nper,
            double pmt,
            double pv,
            int when = 1)
        {
            if (when != 1 
                && when != 0)
            {
                throw new AiofFriendlyException(HttpStatusCode.BadRequest,
                    $"Future value 'when' paramter must be either 1 or 0");
            }

            var result = 0.0;

            if (rate == 0)
                result = pv + pmt * nper;
            else
            {
                var first = pv * Math.Pow(1 + rate, nper);
                var numerator = pmt * (1 + rate * when);
                var denominator = rate * (Math.Pow(1 + rate, nper) - 1);

                result = first + numerator / denominator;
            }

            return result;
        }

        public double FutureValue(
            double rate,
            double amount,
            int years)
        {
            var total = 0.0;

            for (int i = 0; i < years; i++)
            {
                var amountIncrease = amount * rate;

                total += amount + amountIncrease;
                amount = amount + amountIncrease;
            }

            return total;
        }
    }
}