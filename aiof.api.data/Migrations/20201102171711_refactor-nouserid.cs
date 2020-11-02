using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class refactornouserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "liability",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "monthly_payment",
                table: "liability",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "years",
                table: "liability",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "asset",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthly_payment",
                table: "liability");

            migrationBuilder.DropColumn(
                name: "years",
                table: "liability");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "liability",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "asset",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
