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

            _context.Assets
                .AddRange(GetFakeAssets());

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

        public IEnumerable<Asset> GetFakeAssets()
        {
            return new List<Asset>
            {
                new Asset()
                {
                    Id = 1,
                    PublicKey = Guid.NewGuid(),
                    Name = "car",
                    Type = "car",
                    Value = 14762.12F,
                    FinanceId = 1
                },
                new Asset()
                {
                    Id = 2,
                    PublicKey = Guid.NewGuid(),
                    Name = "house",
                    Type = "house",
                    Value = 250550F,
                    FinanceId = 1
                }
            };
        }
    }
}
