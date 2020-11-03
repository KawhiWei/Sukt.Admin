using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Sukt.Core.Domain.Models.Migrations
{
    public partial class wzw20200911_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Function",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: false),
                    LinkUrl = table.Column<string>(maxLength: 300, nullable: false),
                    CreatedId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Path = table.Column<string>(maxLength: 200, nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Icon = table.Column<string>(maxLength: 200, nullable: false),
                    ParentNumber = table.Column<string>(nullable: true),
                    Component = table.Column<string>(maxLength: 200, nullable: false),
                    ComponentName = table.Column<string>(maxLength: 200, nullable: false),
                    IsShow = table.Column<bool>(nullable: false, defaultValue: true),
                    Sort = table.Column<int>(nullable: false, defaultValue: 0),
                    ButtonClick = table.Column<string>(maxLength: 200, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    MicroName = table.Column<string>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuFunction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MenuId = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<Guid>(nullable: false),
                    CreatedId = table.Column<Guid>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastModifyId = table.Column<Guid>(nullable: true),
                    LastModifedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFunction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Function");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "MenuFunction");
        }
    }
}