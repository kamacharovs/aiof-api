using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aiof.api.data
{
    public static class UtilsEntityFrameworkCore
    {
        public static PropertyBuilder HasSnakeCaseColumnName(this PropertyBuilder propertyBuilder)
        {
            propertyBuilder.Metadata.SetColumnName(
                propertyBuilder
                    .Metadata
                    .Name
                    .ToSnakeCase());

            return propertyBuilder;
        }

        public static async Task<DbSet<TEntity>> ValidateFrequencyAsync<TEntity>(
            this AiofContext context, 
            string value)
            where TEntity : class
        {
            return await context.Frequencies.AnyAsync(x => x.Name == value) ? context.Set<TEntity>() : throw new AiofFriendlyException(HttpStatusCode.BadRequest, $"Invalid {nameof(Frequency)}");
        }
    }
}
