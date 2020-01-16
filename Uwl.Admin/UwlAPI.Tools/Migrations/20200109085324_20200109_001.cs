using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UwlAPI.Tools.Migrations
{
    public partial class _20200109_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginTime",
                table: "UwlSchedule",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "JobParams",
                table: "UwlSchedule",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobParams",
                table: "UwlSchedule");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginTime",
                table: "UwlSchedule",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
