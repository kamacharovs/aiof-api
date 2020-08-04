﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IAiofRepository
    {
        Task<IUser> GetUserAsync(
            int id,
            bool included = true,
            bool asNoTracking = true);
        Task<IUser> GetUserAsync(string username);
        Task<IUser> AddFinanceAsync(
            int userId,
            UserDto userDto);
    }
}