using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class add_account_type_map : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_liability_type_name1",
                table: "liability_type");

            migrationBuilder.DropIndex(
                name: "IX_goal_type_name1",
                table: "goal_type");

            migrationBuilder.DropIndex(
                name: "IX_asset_type_name1",
                table: "asset_type");

            migrationBuilder.CreateTable(
                name: "account_type",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_type", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "account_type_map",
                columns: table => new
                {
                    account_name = table.Column<string>(maxLength: 100, nullable: false),
                    account_type_name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_type_map", x => x.account_name);
                    table.ForeignKey(
                        name: "FK_account_type_map_account_type_account_type_name",
                        column: x => x.account_type_name,
                        principalTable: "account_type",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_type_map_account_type_name",
                table: "account_type_map",
                column: "account_type_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_type_map");

            migrationBuilder.DropTable(
                name: "account_type");

            migrationBuilder.CreateIndex(
                name: "IX_liability_type_name1",
                table: "liability_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_goal_type_name1",
                table: "goal_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_asset_type_name1",
                table: "asset_type",
                column: "name",
                unique: true);
        }
    }
}
