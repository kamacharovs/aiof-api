using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IBaseRepository<T>
        where T : class
    {
        IQueryable<T> GetEntityQuery();
    }
}