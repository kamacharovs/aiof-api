using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using AspNetCoreRateLimit;

namespace aiof.api.data
{
    public class AiofRateLimitConfiguration : RateLimitConfiguration
    {
        private readonly ITenant _tenant;

        public AiofRateLimitConfiguration(
            ITenant tenant,
            IHttpContextAccessor httpContextAccessor,
            IOptions<IpRateLimitOptions> ipOptions,
            IOptions<ClientRateLimitOptions> clientOptions)
            : base(httpContextAccessor, ipOptions, clientOptions)
        {
            _tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        protected override void RegisterResolvers()
        {
            ClientResolvers.Add(new TenantResolveContributor(_tenant));
        }
    }

    public class TenantResolveContributor : IClientResolveContributor
    {
        private readonly ITenant _tenant;

        public TenantResolveContributor(ITenant tenant)
        {
            _tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        public string ResolveClient()
        {
            return _tenant.TenantId.ToString();
        }
    }
}
