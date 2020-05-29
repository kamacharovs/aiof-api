using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        public virtual DbSet<Finance> Finances { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<LiabilityType> LiabilityTypes { get; set; }
        public virtual DbSet<GoalType> GoalTypes { get; set; }

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
                e.Property(x => x.Username).HasColumnName("username").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Asset>(e =>
            {
                e.ToTable("asset");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasColumnName("type_name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasColumnName("value").IsRequired();
                e.Property(x => x.FinanceId).HasColumnName("finance_id");

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<AssetType>(e =>
            {
                e.ToTable("asset_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable("liability");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasColumnName("type_name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasColumnName("value").IsRequired();
                e.Property(x => x.FinanceId).HasColumnName("finance_id");

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<LiabilityType>(e =>
            {
                e.ToTable("liability_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Goal>(e =>
            {
                e.ToTable("goal");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasColumnName("type_name").HasMaxLength(100).IsRequired();
                e.Property(x => x.Savings).HasColumnName("savings").HasColumnType("boolean");
                e.Property(x => x.FinanceId).HasColumnName("finance_id");

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<GoalType>(e =>
            {
                e.ToTable("goal_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Finance>(e =>
            {
                e.ToTable("finance");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasColumnName("public_key").ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.UserId).HasColumnName("user_id").IsRequired();

                e.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Assets)
                    .WithOne()
                    .HasForeignKey(x => x.FinanceId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Liabilities)
                    .WithOne()
                    .HasForeignKey(x => x.FinanceId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Goals)
                    .WithOne()
                    .HasForeignKey(x => x.FinanceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
