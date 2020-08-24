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
                    MaritalStatus = "single",
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
                    Name = "car",
                    TypeName = "car",
                    Value = 14762.12M,
                    UserId = 1
                },
                new Asset
                {
                    Id = 2,
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
                    TypeName = "house",
                    Value = 999999M,
                    UserId = 1
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



        #region Unit Tests
        public IEnumerable<object[]> GetFakeUsersData(
            bool id = false,
            bool username = false)
        {
            var fakeUsers = GetFakeUsers();

            var toReturn = new List<object[]>();

            if (id)
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
            bool username = false)
        {
            var fakeUserProfiles = _context.UserProfiles
                .Include(x => x.User)
                .AsNoTracking();

            var toReturn = new List<object[]>();

            if (username)
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

            return toReturn;
        }
        #endregion
    }
}
