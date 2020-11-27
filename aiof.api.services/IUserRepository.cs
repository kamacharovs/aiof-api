using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using aiof.api.data;

namespace aiof.api.services
{
    public interface IUserRepository
    {
        Task<IUser> GetAsync(bool asNoTracking = true);
        Task<IUser> GetAsync(
            string username,
            bool asNoTracking = true);
        Task<IUserProfile> GetProfileAsync(bool asNoTracking = true);
        Task<IUser> UpsertAsync(UserDto userDto);
        Task<IUserProfile> UpsertProfileAsync(UserProfileDto userProfileDto);
        Task<ISubscription> GetSubscriptionAsync(
            int id,
            bool asNoTracking = true);
        Task<ISubscription> GetSubscriptionAsync(
            Guid publicKey,
            bool asNoTracking = true);
        Task<IEnumerable<ISubscription>> GetSubscriptionsAsync(bool asNoTracking = true);
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
        Task<IEnumerable<IEducationLevel>> GetEducationLevelsAsync();
        Task<IEnumerable<IMaritalStatus>> GetMaritalStatusesAsync();
        Task<IEnumerable<IResidentialStatus>> GetResidentialStatusesAsync();
        Task<IEnumerable<IGender>> GetGendersAsync();
    }
}