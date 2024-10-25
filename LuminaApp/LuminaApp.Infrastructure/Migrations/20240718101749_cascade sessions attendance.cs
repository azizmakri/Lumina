using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminaApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class cascadesessionsattendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Sessions_SessionFK",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Sessions_sessionFk",
                table: "Evaluations");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Sessions_SessionFK",
                table: "Attendances",
                column: "SessionFK",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Sessions_sessionFk",
                table: "Evaluations",
                column: "sessionFk",
                principalTable: "Sessions",
                principalColumn: "SessionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Sessions_SessionFK",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Sessions_sessionFk",
                table: "Evaluations");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Sessions_SessionFK",
                table: "Attendances",
                column: "SessionFK",
                principalTable: "Sessions",
                principalColumn: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Sessions_sessionFk",
                table: "Evaluations",
                column: "sessionFk",
                principalTable: "Sessions",
                principalColumn: "SessionId");
        }
    }
}
