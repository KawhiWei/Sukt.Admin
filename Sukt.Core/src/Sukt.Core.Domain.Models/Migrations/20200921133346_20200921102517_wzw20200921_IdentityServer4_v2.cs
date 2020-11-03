using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.Domain.Models.Migrations
{
    public partial class _20200921102517_wzw20200921_IdentityServer4_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProtocolType",
                table: "Client",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true,
                oldDefaultValue: "oidc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProtocolType",
                table: "Client",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                defaultValue: "oidc",
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}