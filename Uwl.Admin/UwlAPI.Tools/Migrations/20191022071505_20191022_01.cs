using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UwlAPI.Tools.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class _20191022_01 : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UwlSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedId = table.Column<Guid>(nullable: true),
                    CreatedName = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    UpdateName = table.Column<string>(maxLength: 50, nullable: true),
                    UpdateId = table.Column<Guid>(nullable: true),
                    IsDrop = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    JobGroup = table.Column<string>(nullable: true),
                    Cron = table.Column<string>(nullable: true),
                    AssemblyName = table.Column<string>(nullable: true),
                    ClassName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RunTimes = table.Column<int>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: true),
                    TriggerType = table.Column<int>(nullable: false),
                    IntervalSecond = table.Column<int>(nullable: false),
                    IsStart = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UwlSchedule", x => x.Id);
                });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UwlSchedule");
        }
    }
}
