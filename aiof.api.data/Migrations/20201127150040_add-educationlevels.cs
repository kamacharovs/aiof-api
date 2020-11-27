using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aiof.api.data.Migrations
{
    public partial class addeducationlevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "education_level",
                columns: table => new
                {
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    public_key = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_level", x => x.name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "education_level");
        }
    }
}
