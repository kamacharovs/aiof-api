using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IUserRepository
    {
        Task<IUser> GetUserAsync(
            string username,
            bool asNoTracking = true);
        Task<IUserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true);
        Task<IUser> AddUserProfileAsync(
            string username,
            UserProfileDto userProfileDto);
        Task<IUserProfile> UpdateUserProfileAsync(
            string username,
            UserProfileDto userProfileDto);
    }
}