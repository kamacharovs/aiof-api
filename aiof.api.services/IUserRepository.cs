using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IUserRepository
    {
        Task<IUser> GetAsync(
            int id,
            bool asNoTracking = true);
        Task<IUser> GetUserAsync(
            string username,
            bool asNoTracking = true);
        Task<IUserProfile> GetUserProfileAsync(
            string username,
            bool asNoTracking = true);
        Task<IUser> UpsertAsync(
            int userId,
            UserDto userDto);
        Task<IUser> UpsertUserProfileAsync(
            string username,
            UserProfileDto userProfileDto);
        Task<ISubscription> GetSubscriptionAsync(int id);
        Task<ISubscription> GetSubscriptionAsync(Guid publicKey);
        Task<ISubscription> AddSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<ISubscription> UpdateSubscriptionAsync(
            int id,
            SubscriptionDto subscriptionDto);
        Task DeleteSubscriptionAsync(int id);
        Task<IAccount> GetAccountAsync(int id);
        Task<IEnumerable<IAccountType>> GetAccountTypesAsync();
        Task<IEnumerable<IAccountTypeMap>> GetAccountTypeMapsAsync();
        Task DeleteAccountAsync(int id);
    }
}