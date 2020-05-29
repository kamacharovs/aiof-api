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

        public IEnumerable<AssetType> GetFakeAssetTypes()
        {
            return new List<AssetType>()
            {
                new AssetType()
                {
                    Name = "car"
                },
                new AssetType()
                {
                    Name = "house"
                },
                new AssetType()
                {
                    Name = "investment"
                },
                new AssetType()
                {
                    Name = "stock"
                },
                new AssetType()
                {
                    Name = "cash"
                },
                new AssetType()
                {
                    Name = "other"
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

        public IEnumerable<LiabilityType> GetFakeLiabilityTypes()
        {
            return new List<LiabilityType>()
            {
                new LiabilityType()
                {
                    Name = "personal loan"
                },
                new LiabilityType()
                {
                    Name = "car loan"
                },
                new LiabilityType()
                {
                    Name = "credit card"
                },
                new LiabilityType()
                {
                    Name = "mortgage"
                },
                new LiabilityType()
                {
                    Name = "house renovation"
                },
                new LiabilityType()
                {
                    Name = "other"
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
                    FinanceId = 1
                }
            };
        }
    }
}
