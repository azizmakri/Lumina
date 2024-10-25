using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminaApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class statusnotif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "read",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "read",
                table: "Notifications");
        }
    }
}
