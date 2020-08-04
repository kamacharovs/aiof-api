﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofRepository
    {
        Task<IUser> GetUserAsync(int id);
        Task<IUser> GetUserAsync(string username);
        Task<IUser> AddUserAsync(UserDto userDto);
        Task<bool> IsUserUniqueAsync(string username, string email);
        Task<ILiability> GetLiabilityAsync(int id);
        Task<IEnumerable<ILiabilityType>> GetLiabilityTypesAsync();
        Task<ILiability> AddLiabilityAsync(LiabilityDto liability);
        IAsyncEnumerable<ILiability> AddLiabilitiesAsync(IEnumerable<LiabilityDto> liabilities);
        Task<ILiability> UpdateLiabilityAsync(int id, LiabilityDto liabilityDto);
    }
}