using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace aiof.api.data.Migrations
{
    public partial class useful_documentation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "useful_documentation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_key = table.Column<Guid>(nullable: false),
                    page = table.Column<string>(maxLength: 100, nullable: false),
                    name = table.Column<string>(maxLength: 300, nullable: false),
                    url = table.Column<string>(maxLength: 500, nullable: false),
                    category = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_useful_documentation", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "useful_documentation");
        }
    }
}
