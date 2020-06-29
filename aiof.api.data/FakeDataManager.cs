using System;
using System.Collections.Generic;
using System.Text;

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

            _context.Finances
                .AddRange(GetFakeFinances());

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

            _context.SaveChanges();
        }

        public IEnumerable<User> GetFakeUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    FirstName = "Georgi",
                    LastName = "Kamacharov",
                    Email = "gkama@test.com",
                    Username = "gkama"
                },
                new User()
                {
                    Id = 2,
                    PublicKey = Guid.NewGuid(),
                    FirstName = "Jessie",
                    LastName = "Brown",
                    Email = "jbro@test.com",
                    Username = "jbro"
                }
            };
        }

        public IEnumerable<Finance> GetFakeFinances()
        {
            return new List<Finance>()
            {
                new Finance()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    UserId = 1
                }
            };
        }

        public IEnumerable<Asset> GetFakeAssets()
        {
            return new List<Asset>
            {
                new Asset()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "car",
                    TypeName = "car",
                    Value = 14762.12F,
                    FinanceId = 1
                },
                new Asset()
                {
                    Id = 2,
                    PublicKey = Guid.NewGuid(),
                    Name = "house",
                    TypeName = "house",
                    Value = 250550F,
                    FinanceId = 1
                }
            };
        }

        public IEnumerable<Liability> GetFakeLiabilities()
        {
            return new List<Liability>()
            {
                new Liability()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "car loan",
                    TypeName = "car loan",
                    Value = 24923.99F,
                    FinanceId = 1
                }
            };
        }

        public IEnumerable<Goal> GetFakeGoals()
        {
            return new List<Goal>()
            {
                new Goal()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "savings",
                    TypeName = "long term",
                    Savings = true,
                    FinanceId = 1
                }
            };
        }

        public IEnumerable<AssetType> GetFakeAssetTypes()
        {
            return new List<AssetType>()
            {
                new AssetType()
                {
                    Name = "car",
                    PublicKey = Guid.NewGuid()
                },
                new AssetType()
                {
                    Name = "house",
                    PublicKey = Guid.NewGuid()
                },
                new AssetType()
                {
                    Name = "investment",
                    PublicKey = Guid.NewGuid()
                },
                new AssetType()
                {
                    Name = "stock",
                    PublicKey = Guid.NewGuid()
                },
                new AssetType()
                {
                    Name = "cash",
                    PublicKey = Guid.NewGuid()
                },
                new AssetType()
                {
                    Name = "other",
                    PublicKey = Guid.NewGuid()
                }
            };
        }

        public IEnumerable<LiabilityType> GetFakeLiabilityTypes()
        {
            return new List<LiabilityType>()
            {
                new LiabilityType()
                {
                    Name = "personal loan",
                    PublicKey = Guid.NewGuid()
                },
                new LiabilityType()
                {
                    Name = "car loan",
                    PublicKey = Guid.NewGuid()
                },
                new LiabilityType()
                {
                    Name = "credit card",
                    PublicKey = Guid.NewGuid()
                },
                new LiabilityType()
                {
                    Name = "mortgage",
                    PublicKey = Guid.NewGuid()
                },
                new LiabilityType()
                {
                    Name = "house renovation",
                    PublicKey = Guid.NewGuid()
                },
                new LiabilityType()
                {
                    Name = "other",
                    PublicKey = Guid.NewGuid()
                }
            };
        }

        public IEnumerable<GoalType> GetFakeGoalTypes()
        {
            return new List<GoalType>()
            {
                new GoalType()
                {
                    Name = "short term",
                    PublicKey = Guid.NewGuid()
                },
                new GoalType()
                {
                    Name = "long term",
                    PublicKey = Guid.NewGuid()
                }
            };
        }
    }
}
