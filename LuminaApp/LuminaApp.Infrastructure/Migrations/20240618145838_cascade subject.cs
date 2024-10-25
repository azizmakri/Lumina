using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminaApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadesubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Subjects_SubjectFK",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Subjects_SubjectFK",
                table: "Sessions",
                column: "SubjectFK",
                principalTable: "Subjects",
                principalColumn: "subjectId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Subjects_SubjectFK",
                table: "Sessions");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Subjects_SubjectFK",
                table: "Sessions",
                column: "SubjectFK",
                principalTable: "Subjects",
                principalColumn: "subjectId");
        }
    }
}
