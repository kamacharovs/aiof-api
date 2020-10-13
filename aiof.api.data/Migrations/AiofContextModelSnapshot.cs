﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using aiof.api.data;

namespace aiof.api.data.Migrations
{
    [DbContext(typeof(AiofContext))]
    partial class AiofContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("aiof.api.data.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnName("type_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("account");
                });

            modelBuilder.Entity("aiof.api.data.AccountType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.HasKey("Name");

                    b.ToTable("account_type");
                });

            modelBuilder.Entity("aiof.api.data.AccountTypeMap", b =>
                {
                    b.Property<string>("AccountName")
                        .HasColumnName("account_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AccountTypeName")
                        .IsRequired()
                        .HasColumnName("account_type_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.HasKey("AccountName");

                    b.HasIndex("AccountTypeName");

                    b.ToTable("account_type_map");
                });

            modelBuilder.Entity("aiof.api.data.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnName("type_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnName("value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("asset");
                });

            modelBuilder.Entity("aiof.api.data.AssetType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.HasKey("Name");

                    b.ToTable("asset_type");
                });

            modelBuilder.Entity("aiof.api.data.Frequency", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnName("value")
                        .HasColumnType("integer");

                    b.HasKey("Name");

                    b.ToTable("frequency");
                });

            modelBuilder.Entity("aiof.api.data.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Contribution")
                        .HasColumnName("contribution")
                        .HasColumnType("numeric");

                    b.Property<string>("ContributionFrequencyName")
                        .IsRequired()
                        .HasColumnName("contribution_frequency_name")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<decimal>("CurrentAmount")
                        .HasColumnName("current_amount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("PlannedDate")
                        .HasColumnName("planned_date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnName("type_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ContributionFrequencyName");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("goal");
                });

            modelBuilder.Entity("aiof.api.data.GoalType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.HasKey("Name");

                    b.ToTable("goal_type");
                });

            modelBuilder.Entity("aiof.api.data.Liability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnName("type_name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnName("value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("liability");
                });

            modelBuilder.Entity("aiof.api.data.LiabilityType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.HasKey("Name");

                    b.ToTable("liability_type");
                });

            modelBuilder.Entity("aiof.api.data.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("From")
                        .HasColumnName("from")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PaymentFrequencyName")
                        .IsRequired()
                        .HasColumnName("payment_frequency_name")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<int>("PaymentLength")
                        .HasColumnName("payment_length")
                        .HasColumnType("integer");

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .HasColumnName("url")
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PaymentFrequencyName");

                    b.HasIndex("UserId");

                    b.ToTable("subscription");
                });

            modelBuilder.Entity("aiof.api.data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnName("created")
                        .HasColumnType("timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("first_name")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("last_name")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("aiof.api.data.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Age")
                        .HasColumnName("age")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnName("date_of_birth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EducationLevel")
                        .HasColumnName("education_level")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnName("gender")
                        .HasColumnType("text");

                    b.Property<decimal?>("GrossSalary")
                        .HasColumnName("gross_salary")
                        .HasColumnType("numeric");

                    b.Property<int?>("HouseholdAdults")
                        .HasColumnName("household_adults")
                        .HasColumnType("integer");

                    b.Property<int?>("HouseholdChildren")
                        .HasColumnName("household_children")
                        .HasColumnType("integer");

                    b.Property<decimal?>("HouseholdIncome")
                        .HasColumnName("household_income")
                        .HasColumnType("numeric");

                    b.Property<string>("MaritalStatus")
                        .HasColumnName("marital_status")
                        .HasColumnType("text");

                    b.Property<string>("Occupation")
                        .HasColumnName("occupation")
                        .HasColumnType("text");

                    b.Property<string>("OccupationIndustry")
                        .HasColumnName("occupation_industry")
                        .HasColumnType("text");

                    b.Property<Guid>("PublicKey")
                        .HasColumnName("public_key")
                        .HasColumnType("uuid");

                    b.Property<string>("ResidentialStatus")
                        .HasColumnName("residential_status")
                        .HasColumnType("text");

                    b.Property<decimal?>("RetirementContributionsPreTax")
                        .HasColumnName("retirement_contributions_pre_tax")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_profile");
                });

            modelBuilder.Entity("aiof.api.data.Account", b =>
                {
                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.AccountTypeMap", b =>
                {
                    b.HasOne("aiof.api.data.AccountType", "AccountType")
                        .WithMany()
                        .HasForeignKey("AccountTypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.Asset", b =>
                {
                    b.HasOne("aiof.api.data.AssetType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Assets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("aiof.api.data.Goal", b =>
                {
                    b.HasOne("aiof.api.data.Frequency", "ContributionFrequency")
                        .WithMany()
                        .HasForeignKey("ContributionFrequencyName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aiof.api.data.GoalType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Goals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("aiof.api.data.Liability", b =>
                {
                    b.HasOne("aiof.api.data.LiabilityType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Liabilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("aiof.api.data.Subscription", b =>
                {
                    b.HasOne("aiof.api.data.Frequency", "PaymentFrequency")
                        .WithMany()
                        .HasForeignKey("PaymentFrequencyName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.UserProfile", b =>
                {
                    b.HasOne("aiof.api.data.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("aiof.api.data.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
