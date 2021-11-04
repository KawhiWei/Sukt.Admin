using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.EntityFrameworkCore.Migrations
{
    public partial class default_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenu_Menu_MenuId",
                table: "RoleMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleMenu_Role_RoleId",
                table: "RoleMenu");

            migrationBuilder.DropIndex(
                name: "IX_RoleMenu_MenuId",
                table: "RoleMenu");

            migrationBuilder.DropIndex(
                name: "IX_RoleMenu_RoleId",
                table: "RoleMenu");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "RoleMenu",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "RoleMenu",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RoleId",
                table: "RoleMenu",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "MenuId",
                table: "RoleMenu",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_MenuId",
                table: "RoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenu_RoleId",
                table: "RoleMenu",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenu_Menu_MenuId",
                table: "RoleMenu",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleMenu_Role_RoleId",
                table: "RoleMenu",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
