using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.Domain.Models.Migrations
{
    public partial class suktbaseadmin_default_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "IdentityResource",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "IncludeJwtId",
                table: "Client",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "ApiScope",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "ApiResource",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "IdentityResource",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IncludeJwtId",
                table: "Client",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "ApiScope",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ShowInDiscoveryDocument",
                table: "ApiResource",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValue: true);
        }
    }
}
