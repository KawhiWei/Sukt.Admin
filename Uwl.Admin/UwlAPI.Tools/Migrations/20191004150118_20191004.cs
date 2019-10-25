using Microsoft.EntityFrameworkCore.Migrations;

namespace UwlAPI.Tools.Migrations
{
    public partial class _20191004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "UwlMenus",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentIdArr",
                table: "UwlMenus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentIdArr",
                table: "UwlMenus");

            migrationBuilder.AlterColumn<int>(
                name: "Sort",
                table: "UwlMenus",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
