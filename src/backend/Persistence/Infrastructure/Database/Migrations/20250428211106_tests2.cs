using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class tests2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "notification_days",
                table: "users",
                type: "integer",
                nullable: true,
                defaultValue: 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notification_days",
                table: "users");
        }
    }
}
