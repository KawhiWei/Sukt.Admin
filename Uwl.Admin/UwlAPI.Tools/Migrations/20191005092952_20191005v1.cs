using Microsoft.EntityFrameworkCore.Migrations;

namespace UwlAPI.Tools.Migrations
{
    public partial class _20191005v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizeState",
                table: "UwlSysOrganizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganizeType",
                table: "UwlSysOrganizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentArr",
                table: "UwlSysOrganizes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizeState",
                table: "UwlSysOrganizes");

            migrationBuilder.DropColumn(
                name: "OrganizeType",
                table: "UwlSysOrganizes");

            migrationBuilder.DropColumn(
                name: "ParentArr",
                table: "UwlSysOrganizes");
        }
    }
}
