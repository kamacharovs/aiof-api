using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        [Required]
        public int UserId { get; set; }

        [JsonPropertyName("assets")]
        public List<AssetDto> AssetDtos { get; set; } = new List<AssetDto>();

        [JsonPropertyName("liabilities")]
        public List<LiabilityDto> LiabilityDtos { get; set; } = new List<LiabilityDto>();

        [JsonPropertyName("goals")]
        public List<GoalDto> GoalDtos { get; set; } = new List<GoalDto>();
    }
}
