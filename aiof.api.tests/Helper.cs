using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using aiof.api.data;
using aiof.api.services;

namespace aiof.api.tests
{
    public class Helper<T>
    {
        public T GetRequiredService()
        {
            var provider = Provider();

            provider.GetRequiredService<FakeDataManager>()
                .UseFakeContext();

            return provider.GetRequiredService<T>();
        }

        private IServiceProvider Provider()
        {
            var services = new ServiceCollection();

            services.AddScoped<IAiofRepository, AiofRepository>();
            services.AddScoped<FakeDataManager>();

            services.AddDbContext<AiofContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddLogging();

            return services.BuildServiceProvider();
        }

    }
}
