using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public class AiofContext : DbContext
    {
        public readonly ITenant Tenant;

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDependent> UserDependents { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<LiabilityType> LiabilityTypes { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalTrip> GoalsTrip { get; set; }
        public virtual DbSet<GoalHome> GoalsHome { get; set; }
        public virtual DbSet<GoalCar> GoalsCar { get; set; }
        public virtual DbSet<GoalCollege> GoalsCollege { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<ResidentialStatus> ResidentialStatuses { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<HouseholdAdult> HouseholdAdults { get; set; }
        public virtual DbSet<HouseholdChild> HouseholdChildren { get; set; }
        public virtual DbSet<UsefulDocumentation> UsefulDocumentations { get; set; }

        public AiofContext(DbContextOptions<AiofContext> options, ITenant tenant)
            : base(options)
        {
            Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable(Keys.Entity.User);

                e.HasQueryFilter(x => x.Id == Tenant.UserId);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.FirstName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Email).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Created).HasColumnType("timestamp").HasSnakeCaseColumnName().IsRequired();

                e.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasForeignKey<UserProfile>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                e.HasMany(x => x.Dependents)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

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

            modelBuilder.Entity<UserDependent>(e =>
            {
                e.ToTable(Keys.Entity.UserDependent);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.FirstName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Age).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Email).HasSnakeCaseColumnName().HasMaxLength(200);
                e.Property(x => x.AmountOfSupportProvided).HasSnakeCaseColumnName();
                e.Property(x => x.UserRelationship).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Created).HasColumnType("timestamp").HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<UserProfile>(e =>
            {
                e.ToTable(Keys.Entity.UserProfile);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId);

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

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable(Keys.Entity.Liability);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.MonthlyPayment).HasSnakeCaseColumnName();
                e.Property(x => x.Years).HasSnakeCaseColumnName();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Goal>(e =>
            {
                e.ToTable(Keys.Entity.Goal);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Type).HasSnakeCaseColumnName().HasConversion<string>().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Amount).HasSnakeCaseColumnName();
                e.Property(x => x.CurrentAmount).HasSnakeCaseColumnName();
                e.Property(x => x.MonthlyContribution).HasSnakeCaseColumnName();
                e.Property(x => x.PlannedDate).HasColumnType("timestamp").HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.ProjectedDate).HasColumnType("timestamp").HasSnakeCaseColumnName();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();
            });
            modelBuilder.Entity<GoalTrip>(e =>
            {
                e.ToTable(Keys.Entity.GoalTrip);

                e.Property(x => x.Destination).HasSnakeCaseColumnName().HasMaxLength(300).IsRequired();
                e.Property(x => x.TripType).HasSnakeCaseColumnName().HasConversion<string>().IsRequired();
                e.Property(x => x.Duration).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Travelers).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Flight).HasSnakeCaseColumnName();
                e.Property(x => x.Hotel).HasSnakeCaseColumnName();
                e.Property(x => x.Car).HasSnakeCaseColumnName();
                e.Property(x => x.Food).HasSnakeCaseColumnName();
                e.Property(x => x.Activities).HasSnakeCaseColumnName();
                e.Property(x => x.Other).HasSnakeCaseColumnName();
            });
            modelBuilder.Entity<GoalHome>(e =>
            {
                e.ToTable(Keys.Entity.GoalHome);

                e.Property(x => x.HomeValue).HasSnakeCaseColumnName();
                e.Property(x => x.MortgageRate).HasSnakeCaseColumnName();
                e.Property(x => x.PercentDownPayment).HasSnakeCaseColumnName();
                e.Property(x => x.AnnualInsurance).HasSnakeCaseColumnName();
                e.Property(x => x.AnnualPropertyTax).HasSnakeCaseColumnName();
                e.Property(x => x.RecommendedAmount).HasSnakeCaseColumnName();
            });
            modelBuilder.Entity<GoalCar>(e =>
            {
                e.ToTable(Keys.Entity.GoalCar);

                e.Property(x => x.Year).HasSnakeCaseColumnName();
                e.Property(x => x.Make).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.Model).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.Trim).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.New).HasSnakeCaseColumnName();
                e.Property(x => x.Price).HasSnakeCaseColumnName();
                e.Property(x => x.DesiredMonthlyPayment).HasSnakeCaseColumnName();
                e.Property(x => x.LoanTermMonths).HasSnakeCaseColumnName();
                e.Property(x => x.InterestRate).HasSnakeCaseColumnName();
            });
            modelBuilder.Entity<GoalCollege>(e =>
            {
                e.ToTable(Keys.Entity.GoalCollege);

                e.Property(x => x.CollegeType).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.CostPerYear).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.StudentAge).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Years).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.CollegeName).HasSnakeCaseColumnName().HasMaxLength(300);
                e.Property(x => x.AnnualCostIncrease).HasSnakeCaseColumnName();
                e.Property(x => x.BeginningCollegeAge).HasSnakeCaseColumnName();
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

            modelBuilder.Entity<Subscription>(e =>
            {
                e.ToTable(Keys.Entity.Subscription);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Description).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.Amount).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.PaymentLength).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.From).HasSnakeCaseColumnName().HasMaxLength(200);
                e.Property(x => x.Url).HasSnakeCaseColumnName().HasMaxLength(500);
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<Account>(e =>
            {
                e.ToTable(Keys.Entity.Account);

                e.HasKey(x => x.Id);

                e.HasQueryFilter(x => x.UserId == Tenant.UserId
                    && !x.IsDeleted);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Description).HasSnakeCaseColumnName().HasMaxLength(500).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.IsDeleted).HasSnakeCaseColumnName().IsRequired();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<AccountType>(e =>
            {
                e.ToTable(Keys.Entity.AccountType);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Type).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<EducationLevel>(e =>
            {
                e.ToTable(Keys.Entity.EducationLevel);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<MaritalStatus>(e =>
            {
                e.ToTable(Keys.Entity.MaritalStatus);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<ResidentialStatus>(e =>
            {
                e.ToTable(Keys.Entity.ResidentialStatus);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<Gender>(e =>
            {
                e.ToTable(Keys.Entity.Gender);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<HouseholdAdult>(e =>
            {
                e.ToTable(Keys.Entity.HouseholdAdult);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<HouseholdChild>(e =>
            {
                e.ToTable(Keys.Entity.HouseholdChild);

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<UsefulDocumentation>(e =>
            {
                e.ToTable(Keys.Entity.UsefulDocumentation);

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Page).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(300).IsRequired();
                e.Property(x => x.Url).HasSnakeCaseColumnName().HasMaxLength(500).IsRequired();
                e.Property(x => x.Category).HasSnakeCaseColumnName().HasMaxLength(100);
            });
        }
    }
}
