using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class update_account_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_dependent_user_user_id",
                table: "user_dependent");

            migrationBuilder.DropTable(
                name: "account_type_map");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "account_type",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_account_type_name",
                table: "account",
                column: "type_name");

            migrationBuilder.AddForeignKey(
                name: "FK_account_account_type_type_name",
                table: "account",
                column: "type_name",
                principalTable: "account_type",
                principalColumn: "name",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_dependent_user_user_id",
                table: "user_dependent",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_account_type_type_name",
                table: "account");

            migrationBuilder.DropForeignKey(
                name: "FK_user_dependent_user_user_id",
                table: "user_dependent");

            migrationBuilder.DropIndex(
                name: "IX_account_type_name",
                table: "account");

            migrationBuilder.DropColumn(
                name: "type",
                table: "account_type");

            migrationBuilder.CreateTable(
                name: "account_type_map",
                columns: table => new
                {
                    account_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    account_type_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_user_dependent_user_user_id",
                table: "user_dependent",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
