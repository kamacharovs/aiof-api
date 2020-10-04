using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Http;

namespace aiof.api.data
{
    public class Tenant : ITenant
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        [JsonPropertyName("client_id")]
        public int ClientId { get; set; }

        [JsonPropertyName("public_key")]
        public Guid PublicKey { get; set; }

        [JsonIgnore]
        public string Log { get; set; }

        public Tenant(IHttpContextAccessor httpContextAccessor)
        {
            int userId, clientId;
            Guid publicKey;

            var claimsPrincipal = httpContextAccessor.HttpContext?.User;

            int.TryParse(claimsPrincipal?.FindFirst(Keys.Claim.UserId)?.Value, out userId);
            int.TryParse(claimsPrincipal?.FindFirst(Keys.Claim.ClientId)?.Value, out clientId);
            Guid.TryParse(claimsPrincipal?.FindFirst(Keys.Claim.PublicKey)?.Value, out publicKey);

            this.UserId = userId;
            this.ClientId = clientId;
            this.PublicKey = publicKey;

            this.Log = JsonSerializer.Serialize(this);
        }
    }
}
