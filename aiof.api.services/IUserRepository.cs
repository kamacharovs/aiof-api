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
        Task<IUser> GetAsync(
            string username,
            bool asNoTracking = true);
        Task<IUserProfile> GetProfileAsync(bool asNoTracking = true);
        Task<IUser> UpsertAsync(
            int userId,
            UserDto userDto);
        Task<IUserProfile> UpsertProfileAsync(
            int userId,
            UserProfileDto userProfileDto);
        Task<ISubscription> GetSubscriptionAsync(
            int id,
            bool asNoTracking = true);
        Task<ISubscription> GetSubscriptionAsync(
            Guid publicKey,
            bool asNoTracking = true);
        Task<ISubscription> AddSubscriptionAsync(SubscriptionDto subscriptionDto);
        Task<ISubscription> UpdateSubscriptionAsync(
            int id,
            SubscriptionDto subscriptionDto);
        Task DeleteSubscriptionAsync(int id);
        Task<IAccount> GetAccountAsync(
            int id,
            bool asNoTracking = true);
        Task<IEnumerable<IAccountType>> GetAccountTypesAsync();
        Task<IEnumerable<IAccountTypeMap>> GetAccountTypeMapsAsync();
        Task<IAccount> AddAccountAsync(AccountDto accountDto);
        Task<IAccount> UpdateAccountAsync(
            int id,
            AccountDto accountDto);
        Task DeleteAccountAsync(int id);
    }
}