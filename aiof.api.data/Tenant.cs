using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace aiof.api.data
{
    public class Tenant : ITenant
    {
        public IHttpContextAccessor _context { get; set; }

        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public Guid? PublicKey { get; set; }

        public Tenant(IHttpContextAccessor httpContextAccessor)
        {
            _context = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            int userId, clientId;
            Guid publicKey;

            int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(Keys.Claim.UserId)?.Value, out userId);
            int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(Keys.Claim.ClientId)?.Value, out clientId);
            Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirst(Keys.Claim.PublicKey)?.Value, out publicKey);

            this.UserId = userId;
            this.ClientId = clientId;
            this.PublicKey = publicKey;
        }
    }
}
