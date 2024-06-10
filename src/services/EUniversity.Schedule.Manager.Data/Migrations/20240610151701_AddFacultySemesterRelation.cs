using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniversity.Schedule.Manager.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFacultySemesterRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FacultyId",
                table: "Semesters",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_FacultyId",
                table: "Semesters",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Faculties_FacultyId",
                table: "Semesters",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Faculties_FacultyId",
                table: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_FacultyId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Semesters");
        }
    }
}
