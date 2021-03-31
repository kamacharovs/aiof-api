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
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("aiof.api.data.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("type_name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("account");
                });

            modelBuilder.Entity("aiof.api.data.AccountType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("type");

                    b.HasKey("Name");

                    b.ToTable("account_type");
                });

            modelBuilder.Entity("aiof.api.data.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("City")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("country");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("State")
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("state");

                    b.Property<string>("StreetLine1")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("street_line1");

                    b.Property<string>("StreetLine2")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("street_line2");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer")
                        .HasColumnName("user_profile_id");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("zip_code");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("address");
                });

            modelBuilder.Entity("aiof.api.data.Asset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("type_name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("asset");
                });

            modelBuilder.Entity("aiof.api.data.AssetType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("asset_type");
                });

            modelBuilder.Entity("aiof.api.data.EducationLevel", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("education_level");
                });

            modelBuilder.Entity("aiof.api.data.Gender", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("gender");
                });

            modelBuilder.Entity("aiof.api.data.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<decimal?>("CurrentAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("current_amount");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<decimal?>("MonthlyContribution")
                        .HasColumnType("numeric")
                        .HasColumnName("monthly_contribution");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<DateTime>("PlannedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("planned_date");

                    b.Property<DateTime?>("ProjectedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("projected_date");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("goal");
                });

            modelBuilder.Entity("aiof.api.data.HouseholdAdult", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("value");

                    b.HasKey("Name");

                    b.ToTable("household_adult");
                });

            modelBuilder.Entity("aiof.api.data.HouseholdChild", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<int>("Value")
                        .HasColumnType("integer")
                        .HasColumnName("value");

                    b.HasKey("Name");

                    b.ToTable("household_child");
                });

            modelBuilder.Entity("aiof.api.data.Liability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<decimal?>("MonthlyPayment")
                        .HasColumnType("numeric")
                        .HasColumnName("monthly_payment");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("type_name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("value");

                    b.Property<int?>("Years")
                        .HasColumnType("integer")
                        .HasColumnName("years");

                    b.HasKey("Id");

                    b.HasIndex("TypeName");

                    b.HasIndex("UserId");

                    b.ToTable("liability");
                });

            modelBuilder.Entity("aiof.api.data.LiabilityType", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("liability_type");
                });

            modelBuilder.Entity("aiof.api.data.MaritalStatus", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("marital_status");
                });

            modelBuilder.Entity("aiof.api.data.ResidentialStatus", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Name");

                    b.ToTable("residential_status");
                });

            modelBuilder.Entity("aiof.api.data.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("amount");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("From")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("from");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.Property<int>("PaymentLength")
                        .HasColumnType("integer")
                        .HasColumnName("payment_length");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("Url")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("url");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("subscription");
                });

            modelBuilder.Entity("aiof.api.data.UsefulDocumentation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Category")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("category");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("name");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("page");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("url");

                    b.HasKey("Id");

                    b.ToTable("useful_documentation");
                });

            modelBuilder.Entity("aiof.api.data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("aiof.api.data.UserDependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<decimal>("AmountOfSupportProvided")
                        .HasColumnType("numeric")
                        .HasColumnName("amount_of_support_provided");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnName("created");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("last_name");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("UserRelationship")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("user_relationship");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("user_dependent");
                });

            modelBuilder.Entity("aiof.api.data.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("EducationLevel")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("education_level");

                    b.Property<string>("Gender")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("gender");

                    b.Property<decimal?>("GrossSalary")
                        .HasColumnType("numeric")
                        .HasColumnName("gross_salary");

                    b.Property<int?>("HouseholdAdults")
                        .HasColumnType("integer")
                        .HasColumnName("household_adults");

                    b.Property<int?>("HouseholdChildren")
                        .HasColumnType("integer")
                        .HasColumnName("household_children");

                    b.Property<decimal?>("HouseholdIncome")
                        .HasColumnType("numeric")
                        .HasColumnName("household_income");

                    b.Property<string>("MaritalStatus")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("marital_status");

                    b.Property<string>("Occupation")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("occupation");

                    b.Property<string>("OccupationIndustry")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("occupation_industry");

                    b.Property<Guid>("PublicKey")
                        .HasColumnType("uuid")
                        .HasColumnName("public_key");

                    b.Property<string>("ResidentialStatus")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("residential_status");

                    b.Property<decimal?>("RetirementContributionsPreTax")
                        .HasColumnType("numeric")
                        .HasColumnName("retirement_contributions_pre_tax");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_profile");
                });

            modelBuilder.Entity("aiof.api.data.GoalCar", b =>
                {
                    b.HasBaseType("aiof.api.data.Goal");

                    b.Property<decimal?>("DesiredMonthlyPayment")
                        .HasColumnType("numeric")
                        .HasColumnName("desired_monthly_payment");

                    b.Property<decimal?>("InterestRate")
                        .HasColumnType("numeric")
                        .HasColumnName("interest_rate");

                    b.Property<int?>("LoanTermMonths")
                        .HasColumnType("integer")
                        .HasColumnName("loan_term_months");

                    b.Property<string>("Make")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("make");

                    b.Property<string>("Model")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("model");

                    b.Property<bool?>("New")
                        .HasColumnType("boolean")
                        .HasColumnName("new");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<string>("Trim")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("trim");

                    b.Property<int?>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.ToTable("goal_car");
                });

            modelBuilder.Entity("aiof.api.data.GoalCollege", b =>
                {
                    b.HasBaseType("aiof.api.data.Goal");

                    b.Property<decimal?>("AnnualCostIncrease")
                        .HasColumnType("numeric")
                        .HasColumnName("annual_cost_increase");

                    b.Property<int?>("BeginningCollegeAge")
                        .HasColumnType("integer")
                        .HasColumnName("beginning_college_age");

                    b.Property<string>("CollegeName")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("college_name");

                    b.Property<string>("CollegeType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("college_type");

                    b.Property<decimal>("CostPerYear")
                        .HasColumnType("numeric")
                        .HasColumnName("cost_per_year");

                    b.Property<int>("StudentAge")
                        .HasColumnType("integer")
                        .HasColumnName("student_age");

                    b.Property<int>("Years")
                        .HasColumnType("integer")
                        .HasColumnName("years");

                    b.ToTable("goal_college");
                });

            modelBuilder.Entity("aiof.api.data.GoalHome", b =>
                {
                    b.HasBaseType("aiof.api.data.Goal");

                    b.Property<decimal?>("AnnualInsurance")
                        .HasColumnType("numeric")
                        .HasColumnName("annual_insurance");

                    b.Property<decimal?>("AnnualPropertyTax")
                        .HasColumnType("numeric")
                        .HasColumnName("annual_property_tax");

                    b.Property<decimal?>("HomeValue")
                        .HasColumnType("numeric")
                        .HasColumnName("home_value");

                    b.Property<decimal?>("MortgageRate")
                        .HasColumnType("numeric")
                        .HasColumnName("mortgage_rate");

                    b.Property<decimal?>("PercentDownPayment")
                        .HasColumnType("numeric")
                        .HasColumnName("percent_down_payment");

                    b.Property<decimal?>("RecommendedAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("recommended_amount");

                    b.ToTable("goal_home");
                });

            modelBuilder.Entity("aiof.api.data.GoalTrip", b =>
                {
                    b.HasBaseType("aiof.api.data.Goal");

                    b.Property<decimal?>("Activities")
                        .HasColumnType("numeric")
                        .HasColumnName("activities");

                    b.Property<decimal?>("Car")
                        .HasColumnType("numeric")
                        .HasColumnName("car");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("destination");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision")
                        .HasColumnName("duration");

                    b.Property<decimal?>("Flight")
                        .HasColumnType("numeric")
                        .HasColumnName("flight");

                    b.Property<decimal?>("Food")
                        .HasColumnType("numeric")
                        .HasColumnName("food");

                    b.Property<decimal?>("Hotel")
                        .HasColumnType("numeric")
                        .HasColumnName("hotel");

                    b.Property<decimal?>("Other")
                        .HasColumnType("numeric")
                        .HasColumnName("other");

                    b.Property<int>("Travelers")
                        .HasColumnType("integer")
                        .HasColumnName("travelers");

                    b.Property<string>("TripType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("trip_type");

                    b.ToTable("goal_trip");
                });

            modelBuilder.Entity("aiof.api.data.Account", b =>
                {
                    b.HasOne("aiof.api.data.AccountType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeName")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("aiof.api.data.Address", b =>
                {
                    b.HasOne("aiof.api.data.UserProfile", null)
                        .WithOne("PhysicalAddress")
                        .HasForeignKey("aiof.api.data.Address", "UserProfileId")
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("aiof.api.data.Goal", b =>
                {
                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Goals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("aiof.api.data.Subscription", b =>
                {
                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.UserDependent", b =>
                {
                    b.HasOne("aiof.api.data.User", null)
                        .WithMany("Dependents")
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

                    b.Navigation("User");
                });

            modelBuilder.Entity("aiof.api.data.GoalCar", b =>
                {
                    b.HasOne("aiof.api.data.Goal", null)
                        .WithOne()
                        .HasForeignKey("aiof.api.data.GoalCar", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.GoalCollege", b =>
                {
                    b.HasOne("aiof.api.data.Goal", null)
                        .WithOne()
                        .HasForeignKey("aiof.api.data.GoalCollege", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.GoalHome", b =>
                {
                    b.HasOne("aiof.api.data.Goal", null)
                        .WithOne()
                        .HasForeignKey("aiof.api.data.GoalHome", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.GoalTrip", b =>
                {
                    b.HasOne("aiof.api.data.Goal", null)
                        .WithOne()
                        .HasForeignKey("aiof.api.data.GoalTrip", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aiof.api.data.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Assets");

                    b.Navigation("Dependents");

                    b.Navigation("Goals");

                    b.Navigation("Liabilities");

                    b.Navigation("Profile")
                        .IsRequired();

                    b.Navigation("Subscriptions");
                });

            modelBuilder.Entity("aiof.api.data.UserProfile", b =>
                {
                    b.Navigation("PhysicalAddress");
                });
#pragma warning restore 612, 618
        }
    }
}
