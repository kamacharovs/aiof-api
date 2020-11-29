using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class add_profile_options : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gender",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gender", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "household_adult",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_adult", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "household_child",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false),
                    value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_child", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "marital_status",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marital_status", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "residential_status",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_residential_status", x => x.name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "gender");

            migrationBuilder.DropTable(
                name: "household_adult");

            migrationBuilder.DropTable(
                name: "household_child");

            migrationBuilder.DropTable(
                name: "marital_status");

            migrationBuilder.DropTable(
                name: "residential_status");
        }
    }
}
