using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminaApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationevaluationsandusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentFk",
                table: "Evaluations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentFk",
                table: "Evaluations",
                column: "StudentFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_AspNetUsers_StudentFk",
                table: "Evaluations",
                column: "StudentFk",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_AspNetUsers_StudentFk",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentFk",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "StudentFk",
                table: "Evaluations");
        }
    }
}
