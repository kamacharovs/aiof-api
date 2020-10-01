using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace aiof.api.data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asset_type",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_type", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "frequency",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 20, nullable: false),
                    public_key = table.Column<Guid>(nullable: false),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frequency", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "goal_type",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_type", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "liability_type",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_liability_type", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(maxLength: 200, nullable: false),
                    last_name = table.Column<string>(maxLength: 200, nullable: false),
                    email = table.Column<string>(maxLength: 200, nullable: false),
                    username = table.Column<string>(maxLength: 200, nullable: false),
                    created = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: false),
                    type_name = table.Column<string>(maxLength: 100, nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "asset",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    type_name = table.Column<string>(maxLength: 100, nullable: false),
                    value = table.Column<decimal>(nullable: false),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset", x => x.id);
                    table.ForeignKey(
                        name: "FK_asset_asset_type_type_name",
                        column: x => x.type_name,
                        principalTable: "asset_type",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_asset_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goal",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    type_name = table.Column<string>(maxLength: 100, nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    current_amount = table.Column<decimal>(nullable: false),
                    contribution = table.Column<decimal>(nullable: false),
                    contribution_frequency_name = table.Column<string>(maxLength: 20, nullable: false),
                    planned_date = table.Column<DateTime>(nullable: true),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal", x => x.id);
                    table.ForeignKey(
                        name: "FK_goal_frequency_contribution_frequency_name",
                        column: x => x.contribution_frequency_name,
                        principalTable: "frequency",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goal_goal_type_type_name",
                        column: x => x.type_name,
                        principalTable: "goal_type",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_goal_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "liability",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    type_name = table.Column<string>(maxLength: 100, nullable: false),
                    value = table.Column<decimal>(nullable: false),
                    user_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_liability", x => x.id);
                    table.ForeignKey(
                        name: "FK_liability_liability_type_type_name",
                        column: x => x.type_name,
                        principalTable: "liability_type",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_liability_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    payment_frequency_name = table.Column<string>(maxLength: 20, nullable: false),
                    payment_length = table.Column<int>(nullable: false),
                    from = table.Column<string>(maxLength: 200, nullable: true),
                    url = table.Column<string>(maxLength: 500, nullable: true),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => x.id);
                    table.ForeignKey(
                        name: "FK_subscription_frequency_payment_frequency_name",
                        column: x => x.payment_frequency_name,
                        principalTable: "frequency",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_subscription_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    gender = table.Column<string>(nullable: true),
                    date_of_birth = table.Column<DateTime>(nullable: true),
                    age = table.Column<int>(nullable: true),
                    occupation = table.Column<string>(nullable: true),
                    occupation_industry = table.Column<string>(nullable: true),
                    gross_salary = table.Column<decimal>(nullable: true),
                    marital_status = table.Column<string>(nullable: true),
                    education_level = table.Column<string>(nullable: true),
                    residential_status = table.Column<string>(nullable: true),
                    household_income = table.Column<decimal>(nullable: true),
                    household_adults = table.Column<int>(nullable: true),
                    household_children = table.Column<int>(nullable: true),
                    retirement_contributions_pre_tax = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_profile_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_type_name",
                table: "asset",
                column: "type_name");

            migrationBuilder.CreateIndex(
                name: "IX_asset_user_id",
                table: "asset",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_type_name1",
                table: "asset_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_goal_contribution_frequency_name",
                table: "goal",
                column: "contribution_frequency_name");

            migrationBuilder.CreateIndex(
                name: "IX_goal_type_name",
                table: "goal",
                column: "type_name");

            migrationBuilder.CreateIndex(
                name: "IX_goal_user_id",
                table: "goal",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_goal_type_name1",
                table: "goal_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_liability_type_name",
                table: "liability",
                column: "type_name");

            migrationBuilder.CreateIndex(
                name: "IX_liability_user_id",
                table: "liability",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_liability_type_name1",
                table: "liability_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscription_payment_frequency_name",
                table: "subscription",
                column: "payment_frequency_name");

            migrationBuilder.CreateIndex(
                name: "IX_subscription_user_id",
                table: "subscription",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_user_id",
                table: "user_profile",
                column: "user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "asset");

            migrationBuilder.DropTable(
                name: "goal");

            migrationBuilder.DropTable(
                name: "liability");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "user_profile");

            migrationBuilder.DropTable(
                name: "asset_type");

            migrationBuilder.DropTable(
                name: "goal_type");

            migrationBuilder.DropTable(
                name: "liability_type");

            migrationBuilder.DropTable(
                name: "frequency");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
