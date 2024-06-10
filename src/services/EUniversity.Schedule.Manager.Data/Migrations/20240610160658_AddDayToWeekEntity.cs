using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniversity.Schedule.Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDayToWeekEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Weeks_WeekId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "WeekId",
                table: "Lessons",
                newName: "DayId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_WeekId",
                table: "Lessons",
                newName: "IX_Lessons_DayId");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    WeekId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Weeks_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Weeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_WeekId",
                table: "Days",
                column: "WeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Days_DayId",
                table: "Lessons",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Days_DayId",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.RenameColumn(
                name: "DayId",
                table: "Lessons",
                newName: "WeekId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_DayId",
                table: "Lessons",
                newName: "IX_Lessons_WeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Weeks_WeekId",
                table: "Lessons",
                column: "WeekId",
                principalTable: "Weeks",
                principalColumn: "Id");
        }
    }
}
