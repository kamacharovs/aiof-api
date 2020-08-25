using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IBaseRepository
    {
        IQueryable<T> GetEntityQuery<T>(bool asNoTracking = true)
            where T : class, IPublicKeyId;
        Task<T> GetEntityAsync<T>(int id)
            where T : class, IPublicKeyId;
        Task<T> GetEntityAsync<T>(Guid publicKey)
            where T : class, IPublicKeyId;
        Task<T> GetEntityAsync<T>(string publicKey)
            where T : class, IPublicKeyId;
        Task DeleteEntityAsync<T>(int id)
            where T : class, IPublicKeyId;
    }
}