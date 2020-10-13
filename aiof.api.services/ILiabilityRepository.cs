using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface ILiabilityRepository
    {
        Task<ILiability> GetAsync(
            int id,
            bool asNoTracking = true);
        Task<ILiability> GetAsync(LiabilityDto liabilityDto);
        Task<IEnumerable<ILiabilityType>> GetTypesAsync();
        Task<ILiability> AddAsync(LiabilityDto liabilityDto);
        IAsyncEnumerable<ILiability> AddAsync(IEnumerable<LiabilityDto> liabilityDtos);
        Task<ILiability> UpdateAsync(
            int id, 
            LiabilityDto liabilityDto);
        Task<ILiabilityType> AddTypeAsync(string name);
        Task DeleteAsync(int id);
    }
}