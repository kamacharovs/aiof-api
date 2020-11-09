using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aiof.api.data
{
    public class FakeDataManager
    {
        private readonly AiofContext _context;

        public FakeDataManager(AiofContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void UseFakeContext()
        {
            _context.Users
                .AddRange(GetFakeUsers());

            _context.UserProfiles
                .AddRange(GetFakeUserProfiles());

            _context.AssetTypes
                .AddRange(GetFakeAssetTypes());

            _context.Assets
                .AddRange(GetFakeAssets());

            _context.LiabilityTypes
                .AddRange(GetFakeLiabilityTypes());

            _context.Liabilities
                .AddRange(GetFakeLiabilities());

            _context.GoalTypes
                .AddRange(GetFakeGoalTypes());

            _context.Goals
                .AddRange(GetFakeGoals());

            _context.Frequencies
                .AddRange(GetFakeFrequencies());

            _context.Subscriptions
                .AddRange(GetFakeSubscriptions());

            _context.Accounts
                .AddRange(GetFakeAccounts());

            _context.AccountTypes
                .AddRange(GetFakeAccountTypes());

            _context.AccountTypeMaps
                .AddRange(GetFakeAccountTypeMaps());

            _context.SaveChanges();
        }

        public IEnumerable<User> GetFakeUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    PublicKey = Guid.Parse("581f3ce6-cf2a-42a5-828f-157a2bfab763"),
                    FirstName = "Georgi",
                    LastName = "Kamacharov",
                    Email = "gkama@test.com",
                    Username = "gkama"
                },
                new User
                {
                    Id = 2,
                    PublicKey = Guid.Parse("8e17276c-88ac-43bd-a9e8-5fdf5381dbd5"),
                    FirstName = "Jessie",
                    LastName = "Brown",
                    Email = "jessie@test.com",
                    Username = "jbro"
                },
                new User
                {
                    Id = 3,
                    PublicKey = Guid.Parse("7c135230-2889-4cbb-bb0e-ab4237d89367"),
                    FirstName = "George",
                    LastName = "Best",
                    Email = "george.best@auth.com",
                    Username = "gbest"
                }
            };
        }

        public IEnumerable<UserProfile> GetFakeUserProfiles()
        {
            return new List<UserProfile>
            {
                new UserProfile
                {
                    Id = 1,
                    UserId = 1,
                    Gender = "Male",
                    Occupation = "Sr. Software Engineer",
                    OccupationIndustry = "IT"
                }
            };
        }

        public IEnumerable<Asset> GetFakeAssets()
        {
            return new List<Asset>
            {
                new Asset
                {
                    Id = 1,
                    PublicKey = Guid.Parse("1ada5134-0290-4ec6-9933-53040906b255"),
                    Name = "car",
                    TypeName = "car",
                    Value = 14762.12M,
                    UserId = 1
                },
                new Asset
                {
                    Id = 2,
                    PublicKey = Guid.Parse("242948e5-6760-43c6-b6ff-21c40de3f9af"),
                    Name = "house",
                    TypeName = "house",
                    Value = 250550M,
                    UserId = 1
                },
                new Asset
                {
                    Id = 3,
                    PublicKey = Guid.Parse("dbf79a48-0504-4bd0-ad00-8cbc3044e585"),
                    Name = "hardcoded guid",
                    TypeName = "investment",
                    Value = 999999M,
                    UserId = 1
                },
                new Asset
                {
                    Id = 4,
                    PublicKey = Guid.Parse("97bedb5b-c49e-484a-8bd0-1d7cb474e217"),
                    Name = "asset",
                    TypeName = "cash",
                    Value = 99M,
                    UserId = 2
                }
            };
        }

        public IEnumerable<Liability> GetFakeLiabilities()
        {
            return new List<Liability>
            {
                new Liability
                {
                    Id = 1,
                    Name = "car loan",
                    TypeName = "car loan",
                    Value = 24923.99M,
                    UserId = 1
                }
            };
        }

        public IEnumerable<Goal> GetFakeGoals()
        {
            return new List<Goal>
            {
                new Goal
                {
                    Id = 1,
                    PublicKey = Guid.Parse("446b2d9b-6d63-4021-946c-d9b0fd99d3fe"),
                    Name = "buy a home by 2021",
                    Amount = 345000M,
                    CurrentAmount = 50000M,
                    Contribution = 2000M,
                    ContributionFrequencyName = "monthly",
                    TypeName = "buy a home",
                    UserId = 1
                }
            };
        }

        public IEnumerable<AssetType> GetFakeAssetTypes()
        {
            return new List<AssetType>
            {
                new AssetType
                {
                    Name = "car"
                },
                new AssetType
                {
                    Name = "house"
                },
                new AssetType
                {
                    Name = "investment"
                },
                new AssetType
                {
                    Name = "stock"
                },
                new AssetType
                {
                    Name = "cash"
                },
                new AssetType
                {
                    Name = "other"
                }
            };
        }

        public IEnumerable<LiabilityType> GetFakeLiabilityTypes()
        {
            return new List<LiabilityType>
            {
                new LiabilityType
                {
                    Name = "personal loan"
                },
                new LiabilityType
                {
                    Name = "car loan"
                },
                new LiabilityType
                {
                    Name = "student loan"
                },
                new LiabilityType
                {
                    Name = "credit card"
                },
                new LiabilityType
                {
                    Name = "mortgage"
                },
                new LiabilityType
                {
                    Name = "house renovation"
                },
                new LiabilityType
                {
                    Name = "rv"
                },
                new LiabilityType
                {
                    Name = "other"
                }
            };
        }

        public IEnumerable<GoalType> GetFakeGoalTypes()
        {
            return new List<GoalType>
            {
                new GoalType
                {
                    Name = "crush credit card debt"
                },
                new GoalType
                {
                    Name = "conquer my loans"
                },
                new GoalType
                {
                    Name = "save for a rainy day"
                },
                new GoalType
                {
                    Name = "prepare for retirement"
                },
                new GoalType
                {
                    Name = "buy a home"
                },
                new GoalType
                {
                    Name = "buy a car"
                },
                new GoalType
                {
                    Name = "save for college"
                },
                new GoalType
                {
                    Name = "take a trip"
                },
                new GoalType
                {
                    Name = "improve my home"
                },
                new GoalType
                {
                    Name = "short term"
                },
                new GoalType
                {
                    Name = "long term"
                },
                new GoalType
                {
                    Name = "other"
                }
            };
        }

        public IEnumerable<Frequency> GetFakeFrequencies()
        {
            return new List<Frequency>
            {
                new Frequency
                {
                    Name = "yearly",
                    Value = 1
                },
                new Frequency
                {
                    Name = "monthly",
                    Value = 12
                },
                new Frequency
                {
                    Name = "weekly",
                    Value = 52
                },
                new Frequency
                {
                    Name = "daily",
                    Value = 365
                }
            };
        }

        public IEnumerable<Subscription> GetFakeSubscriptions()
        {
            return new List<Subscription>
            {
                new Subscription
                {
                    Id = 1,
                    PublicKey = Guid.Parse("89a0109a-f255-4b2d-b486-76d9efe6347b"),
                    Name = "Amazon Prime",
                    Description = "Yearly Amazon Prime subscription",
                    Amount = 99M,
                    PaymentFrequencyName = "yearly",
                    PaymentLength = 1,
                    From = "Amazon",
                    Url = "https://amazon.com/",
                    UserId = 1
                },
                new Subscription
                {
                    Id = 2,
                    PublicKey = Guid.Parse("8297bed8-c13e-4ea0-9e23-88e25ec2829d"),
                    Name = "Spotify",
                    Description = "My monthly Spotify subscription",
                    Amount = 10.99M,
                    PaymentFrequencyName = "monthly",
                    PaymentLength = 12,
                    From = "Spotify",
                    Url = "https://spotify.com/",
                    UserId = 1
                },
                new Subscription
                {
                    Id = 3,
                    PublicKey = Guid.Parse("aaa011a0-48d2-4d89-b8fc-3fc22475b564"),
                    Name = "Generic",
                    Description = "My generic subscription",
                    Amount = 15.99M,
                    PaymentFrequencyName = "monthly",
                    PaymentLength = 12,
                    From = "Generic",
                    Url = "https://google.com/",
                    UserId = 1,
                    IsDeleted = true
                },
                new Subscription
                {
                    Id = 4,
                    PublicKey = Guid.Parse("47bd786a-e419-4daa-b18c-7fb7023800f9"),
                    Name = "Spotify",
                    Description = "My monthly Spotify subscription",
                    Amount = 10.99M,
                    PaymentFrequencyName = "monthly",
                    PaymentLength = 12,
                    From = "Generic",
                    Url = "https://spotify.com/",
                    UserId = 2
                }
            };
        }

        public IEnumerable<Account> GetFakeAccounts()
        {
            return new List<Account>
            {
                new Account
                {
                    Id = 1,
                    Name = "BfA bank acount",
                    Description = "Bank Of Amanerica bank acount",
                    TypeName = "bank",
                    UserId = 1
                }
            };
        }

        public IEnumerable<AccountType> GetFakeAccountTypes()
        {
            return new List<AccountType>
            {
                new AccountType
                {
                    Name = "retirement"
                },
                new AccountType
                {
                    Name = "taxable"
                }
            };
        }

        public IEnumerable<AccountTypeMap> GetFakeAccountTypeMaps()
        {
            return new List<AccountTypeMap>
            {
                new AccountTypeMap
                {
                    AccountName = "401(k)",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "401(a)",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "401(b)",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "457",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "IRA",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "Roth IRA",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "Brokerage",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "Checking/Savings",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "Health Savings Account",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "529 Plan",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "SEP IRA",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "Simple IRA",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "Taxable",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "Tax-Deferred",
                    AccountTypeName = "retirement"
                },
                new AccountTypeMap
                {
                    AccountName = "Self Employed Plan",
                    AccountTypeName = "taxable"
                },
                new AccountTypeMap
                {
                    AccountName = "UGMA/UTMA",
                    AccountTypeName = "taxable"
                }
            };
        }


        #region Unit Tests
        public IEnumerable<object[]> GetFakeUsersData(
            bool id = false,
            bool username = false)
        {
            var fakeUsers = GetFakeUsers();
            var toReturn = new List<object[]>();

            if (id
                & username)
            {
                foreach (var fakeUser in fakeUsers)
                    toReturn.Add(new object[] 
                    { 
                        fakeUser.Id,
                        fakeUser.Username
                    });
            }
            else if (id)
            {
                foreach (var fakeUserId in fakeUsers.Select(x => x.Id))
                    toReturn.Add(new object[] 
                    { 
                        fakeUserId
                    });
            }
            else if (username)
            {
                foreach (var fakeUserUsername in fakeUsers.Select(x => x.Username))
                    toReturn.Add(new object[] 
                    { 
                        fakeUserUsername
                    });
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeUserProfilesData(
            bool userId = false,
            bool username = false)
        {
            var fakeUserProfiles = _context.UserProfiles
                .Include(x => x.User)
                .AsNoTracking();

            var toReturn = new List<object[]>();

            if (userId
                && username)
            {
                foreach (var fakeUser in fakeUserProfiles.Select(x => x.User))
                    toReturn.Add(new object[]
                    {
                        fakeUser.Id,
                        fakeUser.Username,
                    });
            }
            else if (userId)
            {
                foreach (var fakeUserId in fakeUserProfiles.Select(x => x.UserId))
                    toReturn.Add(new object[]
                    {
                        fakeUserId
                    });
            }
            else if (username)
            {
                foreach (var fakeUsername in fakeUserProfiles.Select(x => x.User.Username))
                    toReturn.Add(new object[]
                    {
                        fakeUsername
                    });
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeAssetsData(
            bool id = false,
            bool publicKey = false,
            bool name = false,
            bool typeName = false,
            bool value = false,
            bool userId = false)
        {
            var fakeAssets = GetFakeAssets()
                .ToArray();

            var toReturn = new List<object[]>();

            if (name
                && typeName
                && value
                && userId)
            {
                foreach (var fakeAsset in fakeAssets)
                {
                    toReturn.Add(new object[] 
                    { 
                        fakeAsset.Name, 
                        fakeAsset.TypeName, 
                        fakeAsset.Value, 
                        fakeAsset.UserId
                    });
                }
            }
            else if (id
                && userId)
            {
                foreach (var fakeAsset in fakeAssets)
                {
                    toReturn.Add(new object[]
                    {
                        fakeAsset.Id,
                        fakeAsset.UserId
                    });
                }
            }
            else if (typeName
                && userId)
            {
                foreach (var fakeAsset in fakeAssets)
                {
                    toReturn.Add(new object[]
                    {
                        fakeAsset.TypeName,
                        fakeAsset.UserId
                    });
                }
            }
            else if (id)
            {
                foreach (var fakeAssetId in fakeAssets.Select(x => x.Id))
                {
                    toReturn.Add(new object[] 
                    { 
                        fakeAssetId
                    });
                }
            }
            else if (publicKey)
            {
                foreach (var fakeAssetPublicKey in fakeAssets.Select(x => x.PublicKey))
                {
                    toReturn.Add(new object[] 
                    { 
                        fakeAssetPublicKey
                    });
                }
            }
            else if (typeName)
            {
                foreach (var fakeAssetTypeName in fakeAssets.Select(x => x.TypeName).Distinct())
                {
                    toReturn.Add(new object[] 
                    { 
                        fakeAssetTypeName
                    });
                }
            }
            else if (userId)
            {
                foreach (var fakeAssetUserId in fakeAssets.Select(x => x.UserId).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeAssetUserId
                    });
                }
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeLiabilitiesData(
            bool id = false,
            bool userId = false,
            bool typeName = false)
        {
            var fakeLiabilities = GetFakeLiabilities()
                .ToArray();

            var toReturn = new List<object[]>();

            if (id
                && userId)
            {
                foreach (var fakeLiability in fakeLiabilities.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakeLiability.Id,
                        fakeLiability.UserId
                    });
                }
            }
            else if (userId)
            {
                foreach (var fakeLiabilityUserId in fakeLiabilities.Where(x => !x.IsDeleted).Select(x => x.UserId).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeLiabilityUserId
                    });
                }
            }
            else if (typeName)
            {
                foreach (var fakeLiabilityTypeName in fakeLiabilities.Where(x => !x.IsDeleted).Select(x => x.TypeName).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeLiabilityTypeName
                    });
                }
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeGoalsData(
            bool id = false,
            bool userId = false,
            bool typeName = false)
        {
            var fakeGoals = GetFakeGoals()
                .ToArray();

            var toReturn = new List<object[]>();

            if (id
                && userId)
            {
                foreach (var fakeGoal in fakeGoals.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakeGoal.Id,
                        fakeGoal.UserId
                    });
                }
            }
            else if (userId)
            {
                foreach (var fakeGoalUserId in fakeGoals.Where(x => !x.IsDeleted).Select(x => x.UserId).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeGoalUserId
                    });
                }
            }
            else if (typeName)
            {
                foreach (var fakeGoalTypeName in fakeGoals.Where(x => !x.IsDeleted).Select(x => x.TypeName).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeGoalTypeName
                    });
                }
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeSubscriptionsData(
            bool userId = false,
            bool id = false,
            bool publicKey = false)
        {
            var fakeSubscriptions = GetFakeSubscriptions()
                .ToArray();

            var toReturn = new List<object[]>();

            if (userId
                && id)
            {
                foreach (var fakeSubscription in fakeSubscriptions.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakeSubscription.UserId,
                        fakeSubscription.Id
                    });
                }
            }
            else if (userId
                && publicKey)
            {
                foreach (var fakeSubscription in fakeSubscriptions.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakeSubscription.UserId,
                        fakeSubscription.PublicKey
                    });
                }
            }
            else if (id)
            {
                foreach (var fakeSubscriptionId in fakeSubscriptions.Where(x => !x.IsDeleted).Select(x => x.Id))
                {
                    toReturn.Add(new object[]
                    {
                        fakeSubscriptionId
                    });
                }
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeAccountsData(
            bool userId = false,
            bool id = false,
            bool publicKey = false)
        {
            var fakeAccounts = GetFakeAccounts()
                .ToArray();

            var toReturn = new List<object[]>();

            if (userId
                && id)
            {
                foreach (var fakefakeAccount in fakeAccounts.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakefakeAccount.UserId,
                        fakefakeAccount.Id
                    });
                }
            }
            else if (userId
                && publicKey)
            {
                foreach (var fakeAccount in fakeAccounts.Where(x => !x.IsDeleted))
                {
                    toReturn.Add(new object[]
                    {
                        fakeAccount.UserId,
                        fakeAccount.PublicKey
                    });
                }
            }
            else if (id)
            {
                foreach (var fakeAccountId in fakeAccounts.Where(x => !x.IsDeleted).Select(x => x.Id))
                {
                    toReturn.Add(new object[]
                    {
                        fakeAccountId
                    });
                }
            }
            else if (userId)
            {
                foreach (var fakeAccountUserId in fakeAccounts.Where(x => !x.IsDeleted).Select(x => x.UserId))
                {
                    toReturn.Add(new object[]
                    {
                        fakeAccountUserId
                    });
                }
            }

            return toReturn;
        }
        #endregion
    }
}
