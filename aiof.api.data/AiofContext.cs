using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public class AiofContext : DbContext
    {
        public readonly ITenant _tenant;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<LiabilityType> LiabilityTypes { get; set; }
        public virtual DbSet<GoalType> GoalTypes { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<AccountTypeMap> AccountTypeMaps { get; set; }

        public AiofContext(DbContextOptions<AiofContext> options, ITenant tenant)
            : base(options)
        {
            _tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable(Keys.Entity.User);

                e.HasQueryFilter(x => x.Id == _tenant.UserId);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.FirstName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Email).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Username).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Created).HasColumnType("timestamp").HasSnakeCaseColumnName().IsRequired();

                e.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasForeignKey<UserProfile>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                e.HasMany(x => x.Assets)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Liabilities)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Goals)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Subscriptions)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Accounts)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserProfile>(e =>
            {
                e.ToTable(Keys.Entity.UserProfile);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Gender).HasSnakeCaseColumnName();
                e.Property(x => x.DateOfBirth).HasSnakeCaseColumnName();
                e.Property(x => x.Age).HasSnakeCaseColumnName();
                e.Property(x => x.Occupation).HasSnakeCaseColumnName();
                e.Property(x => x.OccupationIndustry).HasSnakeCaseColumnName();
                e.Property(x => x.GrossSalary).HasSnakeCaseColumnName();
                e.Property(x => x.MaritalStatus).HasSnakeCaseColumnName();
                e.Property(x => x.EducationLevel).HasSnakeCaseColumnName();
                e.Property(x => x.ResidentialStatus).HasSnakeCaseColumnName();
                e.Property(x => x.HouseholdIncome).HasSnakeCaseColumnName();
                e.Property(x => x.HouseholdAdults).HasSnakeCaseColumnName();
                e.Property(x => x.HouseholdChildren).HasSnakeCaseColumnName();
                e.Property(x => x.RetirementContributionsPreTax).HasSnakeCaseColumnName();
            });

            modelBuilder.Entity<Asset>(e =>
            {
                e.ToTable(Keys.Entity.Asset);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable(Keys.Entity.Liability);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Goal>(e =>
            {
                e.ToTable(Keys.Entity.Goal);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Amount).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.CurrentAmount).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Contribution).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.ContributionFrequencyName).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PlannedDate).HasSnakeCaseColumnName();
                e.Property(x => x.UserId).HasSnakeCaseColumnName();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();

                e.HasOne(x => x.ContributionFrequency)
                    .WithMany()
                    .HasForeignKey(x => x.ContributionFrequencyName)
                    .IsRequired();
            });

            modelBuilder.Entity<AssetType>(e =>
            {
                e.ToTable(Keys.Entity.AssetType);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<LiabilityType>(e =>
            {
                e.ToTable(Keys.Entity.LiabilityType);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<GoalType>(e =>
            {
                e.ToTable(Keys.Entity.GoalType);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<Frequency>(e =>
            {
                e.ToTable(Keys.Entity.Frequency);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<Subscription>(e =>
            {
                e.ToTable(Keys.Entity.Subscription);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Description).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.Amount).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.PaymentFrequencyName).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired();
                e.Property(x => x.PaymentLength).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.From).HasSnakeCaseColumnName().HasMaxLength(200);
                e.Property(x => x.Url).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName();

                e.HasOne(x => x.PaymentFrequency)
                    .WithMany()
                    .HasForeignKey(x => x.PaymentFrequencyName)
                    .IsRequired();
            });

            modelBuilder.Entity<Account>(e =>
            {
                e.ToTable(Keys.Entity.Account);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == _tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Description).HasSnakeCaseColumnName().HasMaxLength(500).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName();
            });

            modelBuilder.Entity<AccountType>(e =>
            {
                e.ToTable(Keys.Entity.AccountType);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<AccountTypeMap>(e =>
            {
                e.ToTable(Keys.Entity.AccountTypeMap);

                e.HasKey(x => x.AccountName);

                e.Property(x => x.AccountName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.AccountTypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();

                e.HasOne(x => x.AccountType)
                    .WithMany()
                    .HasForeignKey(x => x.AccountTypeName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        }
    }
}
