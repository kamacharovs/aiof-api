using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public class AiofContext : DbContext
    {
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }

        public AiofContext(DbContextOptions<AiofContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(e =>
            {
                e.ToTable("asset");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("publicKey").IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable("liability");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("publicKey").IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            });
        }
    }
}
