using System;
using System.Collections.Generic;
using System.Text;

namespace aiof.api.data
{
    public class Finance : IFinance, IPublicKeyId
    {
        public int Id { get; set; }
        public Guid PublicKey { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Asset> Assets { get; set; } = new List<Asset>();
        public List<Liability> Liabilities { get; set; } = new List<Liability>();
        public List<Goal> Goals { get; set; } = new List<Goal>();
    }

    public class FinanceDto
    {
        public int UserId { get; set; }

        public List<AssetDto> Assets { get; set; } = new List<AssetDto>();
        public List<LiabilityDto> Liabilities { get; set; } = new List<LiabilityDto>();
        public List<GoalDto> Goals { get; set; } = new List<GoalDto>();
    }
}
