using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UwlAPI.Tools.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class _20191002v1 : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "UwlUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizeId",
                table: "UwlUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Depth",
                table: "UwlSysOrganizes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sort",
                table: "UwlSysOrganizes",
                nullable: false,
                defaultValue: 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "UwlUsers");

            migrationBuilder.DropColumn(
                name: "OrganizeId",
                table: "UwlUsers");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "UwlSysOrganizes");

            migrationBuilder.DropColumn(
                name: "Sort",
                table: "UwlSysOrganizes");
        }
    }
}
