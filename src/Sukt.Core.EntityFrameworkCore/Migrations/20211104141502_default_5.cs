using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.EntityFrameworkCore.Migrations
{
    public partial class default_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParentNumber",
                table: "Menu",
                type: "longtext",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MicroName",
                table: "Menu",
                type: "longtext",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParentNumber",
                table: "Menu",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "MicroName",
                table: "Menu",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
