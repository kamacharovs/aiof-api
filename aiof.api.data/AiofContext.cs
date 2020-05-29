using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public class AiofContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }

        public AiofContext(DbContextOptions<AiofContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("user");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.FirstName).HasColumnName("first_name").HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasColumnName("last_name").HasMaxLength(200).IsRequired();
                e.Property(x => x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Asset>(e =>
            {
                e.ToTable("asset");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable("liability");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Goal>(e =>
            {
                e.ToTable("goal");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasColumnName("type").HasMaxLength(100).IsRequired();
            });
        }
    }
}
