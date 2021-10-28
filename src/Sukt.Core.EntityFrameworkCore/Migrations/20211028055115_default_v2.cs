using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sukt.Core.EntityFrameworkCore.Migrations
{
    public partial class default_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserEntityId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_UserEntityId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "UserRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "UserRole",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserEntityId",
                table: "UserRole",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserEntityId",
                table: "UserRole",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
