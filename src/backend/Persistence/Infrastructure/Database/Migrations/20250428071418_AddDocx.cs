using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDocx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioContentKey",
                table: "longreads",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioContentKey",
                table: "longreads");
        }
    }
}
