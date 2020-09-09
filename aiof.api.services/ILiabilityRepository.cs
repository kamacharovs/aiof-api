using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface ILiabilityRepository
    {
        Task<ILiability> GetLiabilityAsync(int id);
        Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync();
        Task<ILiability> AddLiabilityAsync(LiabilityDto liabilityDto);
        IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilityDtos);
        Task<ILiability> UpdateLiabilityAsync(
            int id, 
            LiabilityDto liabilityDto);
        Task<ILiabilityType> AddLiabilityTypeAsync(string name);
    }
}