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
        Task<UserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true);
        Task<UserProfile> UpdateUserProfileAsync(
            string username,
            UserProfileDto userProfileDto);
    }
}