using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IBaseRepository<T>
        where T : class, IPublicKeyId
    {
        IQueryable<T> GetEntityQuery();
        Task<T> GetEntityAsync(int id);
        Task<T> GetEntityAsync(Guid publicKey);
        Task<T> GetEntityAsync(string publicKey);
    }
}