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
    }
}