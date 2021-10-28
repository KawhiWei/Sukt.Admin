using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.EntityFrameworkCore.Migrations
{
    public partial class default_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuFunction_Function_FunctionItemsId",
                table: "MenuFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuFunction_Menu_MenuItemId",
                table: "MenuFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationUser_User_UserId",
                table: "OrganizationUser");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUser_OrganizationId",
                table: "OrganizationUser");

            migrationBuilder.DropIndex(
                name: "IX_OrganizationUser_UserId",
                table: "OrganizationUser");

            migrationBuilder.DropIndex(
                name: "IX_MenuFunction_FunctionItemsId",
                table: "MenuFunction");

            migrationBuilder.DropIndex(
                name: "IX_MenuFunction_MenuItemId",
                table: "MenuFunction");

            migrationBuilder.DropColumn(
                name: "FunctionItemsId",
                table: "MenuFunction");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "MenuFunction");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "OrganizationUser",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationNumber",
                table: "OrganizationUser",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "OrganizationUser",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "FunctionId",
                table: "MenuFunction",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                table: "MenuFunction",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FunctionId",
                table: "MenuFunction");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "MenuFunction");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "OrganizationUser",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationNumber",
                table: "OrganizationUser",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "OrganizationUser",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "FunctionItemsId",
                table: "MenuFunction",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuItemId",
                table: "MenuFunction",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUser_UserId",
                table: "OrganizationUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFunction_FunctionItemsId",
                table: "MenuFunction",
                column: "FunctionItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFunction_MenuItemId",
                table: "MenuFunction",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuFunction_Function_FunctionItemsId",
                table: "MenuFunction",
                column: "FunctionItemsId",
                principalTable: "Function",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuFunction_Menu_MenuItemId",
                table: "MenuFunction",
                column: "MenuItemId",
                principalTable: "Menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_Organization_OrganizationId",
                table: "OrganizationUser",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationUser_User_UserId",
                table: "OrganizationUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
