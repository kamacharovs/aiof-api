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

            _context.UserDependents
                .AddRange(GetFakeUserDependents());

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

            _context.Goals
                .AddRange(GetFakeGoals());

            _context.Subscriptions
                .AddRange(GetFakeSubscriptions());

            _context.Accounts
                .AddRange(GetFakeAccounts());

            _context.AccountTypes
                .AddRange(GetFakeAccountTypes());

            _context.EducationLevels
                .AddRange(GetFakeEducationLevels());

            _context.MaritalStatuses
                .AddRange(GetFakeMaritalStatuses());

            _context.ResidentialStatuses
                .AddRange(GetFakeResidentialStatuses());

            _context.Genders
                .AddRange(GetFakeGenders());

            _context.HouseholdAdults
                .AddRange(GetFakeHouseholdAdults());

            _context.HouseholdChildren
                .AddRange(GetFakeHouseholdChildren());

            _context.UsefulDocumentations
                .AddRange(GetFakeUsefulDocumentations());

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
                    Email = "gkama@test.com"
                },
                new User
                {
                    Id = 2,
                    PublicKey = Guid.Parse("8e17276c-88ac-43bd-a9e8-5fdf5381dbd5"),
                    FirstName = "Jessie",
                    LastName = "Brown",
                    Email = "jessie@test.com"
                },
                new User
                {
                    Id = 3,
                    PublicKey = Guid.Parse("7c135230-2889-4cbb-bb0e-ab4237d89367"),
                    FirstName = "George",
                    LastName = "Best",
                    Email = "george.best@auth.com"
                }
            };
        }

        public IEnumerable<UserDependent> GetFakeUserDependents()
        {
            return new List<UserDependent>
            {
                new UserDependent
                {
                    Id = 1,
                    FirstName = "Zima",
                    LastName = "Kamacharov",
                    Age = 3,
                    Email = "zima.kamacharov@aiof.com",
                    AmountOfSupportProvided = 1500M,
                    UserRelationship = UserRelationships.Child.ToString(),
                    UserId = 1
                },
                new UserDependent
                {
                    Id = 2,
                    FirstName = "Child",
                    LastName = "Childlastname",
                    Age = 12,
                    Email = null,
                    AmountOfSupportProvided = 12000M,
                    UserRelationship = UserRelationships.Son.ToString(),
                    UserId = 2
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
                    Type = GoalType.Generic,
                    UserId = 1,
                    Amount = 3000M,
                    CurrentAmount = 250M,
                    MonthlyContribution = 200M
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
                    TypeName = "Checking/Savings",
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
                    Name = "401(k)",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "401(a)",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "401(b)",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "457",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "IRA",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "Roth IRA",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "Brokerage",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "Checking/Savings",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "Health Savings Account",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "529 Plan",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "SEP IRA",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "Simple IRA",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "Taxable",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "Tax-Deferred",
                    Type = AccountTypes.Retirement.ToString()
                },
                new AccountType
                {
                    Name = "Self Employed Plan",
                    Type = AccountTypes.Taxable.ToString()
                },
                new AccountType
                {
                    Name = "UGMA/UTMA",
                    Type = AccountTypes.Taxable.ToString()
                }
            };
        }

        public IEnumerable<EducationLevel> GetFakeEducationLevels()
        {
            return new List<EducationLevel>
            {
                new EducationLevel
                {
                    Name = "Some high school or less"
                },
                new EducationLevel
                {
                    Name = "High school graduate or equivalent (GED)"
                },
                new EducationLevel
                {
                    Name = "Some college, no degree"
                },
                new EducationLevel
                {
                    Name = "Doctorate"
                }
            };
        }

        public IEnumerable<MaritalStatus> GetFakeMaritalStatuses()
        {
            return new List<MaritalStatus>
            {
                new MaritalStatus
                {
                    Name = "Single"
                },
                new MaritalStatus
                {
                    Name = "Married"
                },
                new MaritalStatus
                {
                    Name = "Living together"
                },
                new MaritalStatus
                {
                    Name = "No longer married"
                }
            };
        }

        public IEnumerable<ResidentialStatus> GetFakeResidentialStatuses()
        {
            return new List<ResidentialStatus>
            {
                new ResidentialStatus
                {
                    Name = "Living with parents / relatives"
                },
                new ResidentialStatus
                {
                    Name = "Couch surfing"
                },
                new ResidentialStatus
                {
                    Name = "Renting with friends"
                },
                new ResidentialStatus
                {
                    Name = "Renting by myself"
                },
                new ResidentialStatus
                {
                    Name = "Campus housing"
                },
                new ResidentialStatus
                {
                    Name = "I own a condo"
                },
                new ResidentialStatus
                {
                    Name = "I own a house"
                }
            };
        }

        public IEnumerable<Gender> GetFakeGenders()
        {
            return new List<Gender>
            {
                new Gender
                {
                    Name = "Male"
                },
                new Gender
                {
                    Name = "Female"
                },
                new Gender
                {
                    Name = "Other"
                }
            };
        }

        public IEnumerable<HouseholdAdult> GetFakeHouseholdAdults()
        {
            return new List<HouseholdAdult>
            {
                new HouseholdAdult
                {
                    Name = "1 adult",
                    Value = 1
                },
                new HouseholdAdult
                {
                    Name = "2 adults",
                    Value = 2
                },
                new HouseholdAdult
                {
                    Name = "3 adults",
                    Value = 3
                },
                new HouseholdAdult
                {
                    Name = "4 adults",
                    Value = 4
                },
                new HouseholdAdult
                {
                    Name = "5 adults",
                    Value = 5
                },
                new HouseholdAdult
                {
                    Name = "6 adults",
                    Value = 6
                }
            };
        }

        public IEnumerable<HouseholdChild> GetFakeHouseholdChildren()
        {
            return new List<HouseholdChild>
            {
                new HouseholdChild
                {
                    Name = "0 children",
                    Value = 0
                },
                new HouseholdChild
                {
                    Name = "1 child",
                    Value = 1
                },
                new HouseholdChild
                {
                    Name = "2 children",
                    Value = 2
                },
                new HouseholdChild
                {
                    Name = "3 children",
                    Value = 3
                },
                new HouseholdChild
                {
                    Name = "4 children",
                    Value = 4
                },
                new HouseholdChild
                {
                    Name = "5 children",
                    Value = 5
                },
                new HouseholdChild
                {
                    Name = "6 children",
                    Value = 6
                }
            };
        }

        public IEnumerable<UsefulDocumentation> GetFakeUsefulDocumentations()
        {
            return new List<UsefulDocumentation>
            {
                new UsefulDocumentation
                {
                    Id = 1,
                    Page = "finance",
                    Name = "What is a financial asset?",
                    Url = "http://google.com",
                    Category = "financial asset"
                },
                new UsefulDocumentation
                {
                    Id = 2,
                    Page = "finance",
                    Name = "What is a financial liability?",
                    Url = "http://google.com",
                    Category = "financial liability"
                }
            };
        }


        #region Unit Tests
        public IEnumerable<object[]> GetFakeUsersData(
            bool id = false,
            bool email = false)
        {
            var fakeUsers = GetFakeUsers();
            var toReturn = new List<object[]>();

            if (id
                & email)
            {
                foreach (var fakeUser in fakeUsers)
                    toReturn.Add(new object[] 
                    { 
                        fakeUser.Id,
                        fakeUser.Email
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
            else if (email)
            {
                foreach (var fakeUserEmail in fakeUsers.Select(x => x.Email))
                    toReturn.Add(new object[] 
                    { 
                        fakeUserEmail
                    });
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeUserDependentsData(
            bool id = false,
            bool userId = false)
        {
            var fakeUserDependents = GetFakeUserDependents();
            var toReturn = new List<object[]>();

            if (id
                && userId)
            {
                foreach (var fakeUserDependent in fakeUserDependents)
                    toReturn.Add(new object[]
                    {
                        fakeUserDependent.Id,
                        fakeUserDependent.UserId
                    });
            }
            else if (id)
            {
                foreach (var fakeUserDependentId in fakeUserDependents.Select(x => x.Id).Distinct())
                    toReturn.Add(new object[]
                    {
                        fakeUserDependentId
                    });
            }
            else if (userId)
            {
                foreach (var fakeUserId in fakeUserDependents.Select(x => x.UserId).Distinct())
                    toReturn.Add(new object[]
                    {
                        fakeUserId
                    });
            }

            return toReturn;
        }

        public IEnumerable<object[]> GetFakeUserProfilesData(
            bool userId = false,
            bool email = false)
        {
            var fakeUserProfiles = _context.UserProfiles
                .Include(x => x.User)
                .AsNoTracking();

            var toReturn = new List<object[]>();

            if (userId
                && email)
            {
                foreach (var fakeUser in fakeUserProfiles.Select(x => x.User))
                    toReturn.Add(new object[]
                    {
                        fakeUser.Id,
                        fakeUser.Email,
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
            else if (email)
            {
                foreach (var fakeEmail in fakeUserProfiles.Select(x => x.User.Email))
                    toReturn.Add(new object[]
                    {
                        fakeEmail
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
                .Where(x => !x.IsDeleted)
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
            bool type = false)
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
            else if (type)
            {
                foreach (var fakeGoalType in fakeGoals.Where(x => !x.IsDeleted).Select(x => x.Type).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeGoalType
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

        public IEnumerable<object[]> GetFakeUsefulDocumentationsData(
            bool page = false,
            bool category = false)
        {
            var fakeUsefulDocumentations = GetFakeUsefulDocumentations()
                .ToArray();

            var toReturn = new List<object[]>();

            if (page)
            {
                foreach (var fakeUsefulDocumentationPage in fakeUsefulDocumentations.Select(x => x.Page).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeUsefulDocumentationPage
                    });
                }
            }
            else if (category)
            {
                foreach (var fakeUsefulDocumentationCategory in fakeUsefulDocumentations.Select(x => x.Category).Distinct())
                {
                    toReturn.Add(new object[]
                    {
                        fakeUsefulDocumentationCategory
                    });
                }
            }

            return toReturn;
        }
        #endregion
    }
}
