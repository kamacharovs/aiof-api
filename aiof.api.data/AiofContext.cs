﻿using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace aiof.api.data
{
    public class AiofContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liability> Liabilities { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<AssetType> AssetTypes { get; set; }
        public virtual DbSet<LiabilityType> LiabilityTypes { get; set; }
        public virtual DbSet<GoalType> GoalTypes { get; set; }
        public virtual DbSet<Frequency> Frequencies { get; set; }

        public AiofContext(DbContextOptions<AiofContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("user");

                e.HasKey(x => x.Id);

                e.HasIndex(x => x.Username)
                    .IsUnique();

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.FirstName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.LastName).HasSnakeCaseColumnName().HasMaxLength(200).IsRequired();
                e.Property(x => x.Email).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Username).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();

                e.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasForeignKey<UserProfile>(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                e.HasMany(x => x.Assets)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Goals)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasMany(x => x.Liabilities)
                    .WithOne()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserProfile>(e =>
            {
                e.ToTable("user_profile");

                e.HasKey(x => x.Id);

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
                e.ToTable("asset");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Liability>(e =>
            {
                e.ToTable("liability");

                e.HasKey(x => x.Id);

                e.Property(x => x.Id).HasSnakeCaseColumnName().ValueGeneratedOnAdd().IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.TypeName).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.UserId).HasSnakeCaseColumnName();

                e.HasOne(x => x.Type)
                    .WithMany()
                    .HasForeignKey(x => x.TypeName)
                    .IsRequired();
            });

            modelBuilder.Entity<Goal>(e =>
            {
                e.ToTable("goal");

                e.HasKey(x => x.Id);

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
                e.ToTable("asset_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<LiabilityType>(e =>
            {
                e.ToTable("liability_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<GoalType>(e =>
            {
                e.ToTable("goal_type");

                e.HasKey(x => x.Name);

                e.HasIndex(x => x.Name)
                    .IsUnique();

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(100).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
            });

            modelBuilder.Entity<Frequency>(e =>
            {
                e.ToTable("frequency");

                e.HasKey(x => x.Name);

                e.Property(x => x.Name).HasSnakeCaseColumnName().HasMaxLength(20).IsRequired();
                e.Property(x => x.PublicKey).HasSnakeCaseColumnName().IsRequired();
                e.Property(x => x.Value).HasSnakeCaseColumnName().IsRequired();
            });
        }
    }
}
