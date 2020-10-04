using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace aiof.api.data
{
    public class Tenant : ITenant
    {
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public Guid PublicKey { get; set; }

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
        }
    }
}
