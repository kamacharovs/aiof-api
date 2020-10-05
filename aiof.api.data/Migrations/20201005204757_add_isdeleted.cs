using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class add_isdeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "subscription",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "liability",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "goal",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "asset",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "subscription");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "liability");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "goal");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "asset");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "account");
        }
    }
}
