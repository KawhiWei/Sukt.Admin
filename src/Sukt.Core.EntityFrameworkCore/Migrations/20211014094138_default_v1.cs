using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.EntityFrameworkCore.Migrations
{
    public partial class default_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessTokenExpire",
                table: "SuktApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessTokenExpire",
                table: "SuktApplications");
        }
    }
}
