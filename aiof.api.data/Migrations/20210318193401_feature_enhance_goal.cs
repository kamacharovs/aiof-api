using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace aiof.api.data.Migrations
{
    public partial class feature_enhance_goal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_goal_frequency_contribution_frequency_name",
                table: "goal");

            migrationBuilder.DropForeignKey(
                name: "FK_goal_goal_type_type_name",
                table: "goal");

            migrationBuilder.DropForeignKey(
                name: "FK_subscription_frequency_payment_frequency_name",
                table: "subscription");

            migrationBuilder.DropTable(
                name: "frequency");

            migrationBuilder.DropTable(
                name: "goal_type");

            migrationBuilder.DropIndex(
                name: "IX_subscription_payment_frequency_name",
                table: "subscription");

            migrationBuilder.DropIndex(
                name: "IX_goal_contribution_frequency_name",
                table: "goal");

            migrationBuilder.DropIndex(
                name: "IX_goal_type_name",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "username",
                table: "user");

            migrationBuilder.DropColumn(
                name: "payment_frequency_name",
                table: "subscription");

            migrationBuilder.DropColumn(
                name: "contribution",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "contribution_frequency_name",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "type_name",
                table: "goal");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "goal",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "planned_date",
                table: "goal",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "current_amount",
                table: "goal",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "goal",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "monthly_contribution",
                table: "goal",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "projected_date",
                table: "goal",
                type: "timestamp",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "goal",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "goal_car",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<int>(type: "integer", nullable: true),
                    make = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    model = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    trim = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    @new = table.Column<bool>(name: "new", type: "boolean", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: true),
                    desired_monthly_payment = table.Column<decimal>(type: "numeric", nullable: true),
                    loan_term_months = table.Column<int>(type: "integer", nullable: true),
                    interest_rate = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_car", x => x.id);
                    table.ForeignKey(
                        name: "FK_goal_car_goal_id",
                        column: x => x.id,
                        principalTable: "goal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goal_college",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    college_type = table.Column<string>(type: "text", nullable: false),
                    cost_per_year = table.Column<decimal>(type: "numeric", nullable: false),
                    student_age = table.Column<int>(type: "integer", nullable: false),
                    years = table.Column<int>(type: "integer", nullable: false),
                    college_name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    annual_cost_increase = table.Column<decimal>(type: "numeric", nullable: true),
                    beginning_college_age = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_college", x => x.id);
                    table.ForeignKey(
                        name: "FK_goal_college_goal_id",
                        column: x => x.id,
                        principalTable: "goal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goal_home",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    home_value = table.Column<decimal>(type: "numeric", nullable: true),
                    mortgage_rate = table.Column<decimal>(type: "numeric", nullable: true),
                    percent_down_payment = table.Column<decimal>(type: "numeric", nullable: true),
                    annual_insurance = table.Column<decimal>(type: "numeric", nullable: true),
                    annual_property_tax = table.Column<decimal>(type: "numeric", nullable: true),
                    recommended_amount = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_home", x => x.id);
                    table.ForeignKey(
                        name: "FK_goal_home_goal_id",
                        column: x => x.id,
                        principalTable: "goal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "goal_trip",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    destination = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    trip_type = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<double>(type: "double precision", nullable: false),
                    travelers = table.Column<int>(type: "integer", nullable: false),
                    flight = table.Column<decimal>(type: "numeric", nullable: true),
                    hotel = table.Column<decimal>(type: "numeric", nullable: true),
                    car = table.Column<decimal>(type: "numeric", nullable: true),
                    food = table.Column<decimal>(type: "numeric", nullable: true),
                    activities = table.Column<decimal>(type: "numeric", nullable: true),
                    other = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_trip", x => x.id);
                    table.ForeignKey(
                        name: "FK_goal_trip_goal_id",
                        column: x => x.id,
                        principalTable: "goal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "goal_car");

            migrationBuilder.DropTable(
                name: "goal_college");

            migrationBuilder.DropTable(
                name: "goal_home");

            migrationBuilder.DropTable(
                name: "goal_trip");

            migrationBuilder.DropColumn(
                name: "monthly_contribution",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "projected_date",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "type",
                table: "goal");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "user",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "payment_frequency_name",
                table: "subscription",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "goal",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "planned_date",
                table: "goal",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<decimal>(
                name: "current_amount",
                table: "goal",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "goal",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "contribution",
                table: "goal",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "contribution_frequency_name",
                table: "goal",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type_name",
                table: "goal",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "frequency",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false),
                    value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frequency", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "goal_type",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goal_type", x => x.name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscription_payment_frequency_name",
                table: "subscription",
                column: "payment_frequency_name");

            migrationBuilder.CreateIndex(
                name: "IX_goal_contribution_frequency_name",
                table: "goal",
                column: "contribution_frequency_name");

            migrationBuilder.CreateIndex(
                name: "IX_goal_type_name",
                table: "goal",
                column: "type_name");

            migrationBuilder.AddForeignKey(
                name: "FK_goal_frequency_contribution_frequency_name",
                table: "goal",
                column: "contribution_frequency_name",
                principalTable: "frequency",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_goal_goal_type_type_name",
                table: "goal",
                column: "type_name",
                principalTable: "goal_type",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subscription_frequency_payment_frequency_name",
                table: "subscription",
                column: "payment_frequency_name",
                principalTable: "frequency",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
